using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CompanyProfile.Core.Context;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CompanyProfile.Data.Context
{
    public sealed class CompanyProfileDbContext : DbContext, IDbContext
    {
        private readonly static ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Core.Configuration.IConfiguration _configuration;
        private readonly IAuditFieldFixer ?_auditFieldFixer;

        private bool IsInMemoryDb => Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory";
        public bool SupportsJson() => !IsInMemoryDb && _configuration.EnvironmentName != "DEV";
        public bool SupportsBulk() => !IsInMemoryDb;

        public CompanyProfileDbContext(DbContextOptions<CompanyProfileDbContext> options,
            Core.Configuration.IConfiguration configuration, IAuditFieldFixer? auditFieldFixer)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _auditFieldFixer = auditFieldFixer;
            //this.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public override int SaveChanges()
        {
            FixupEntities();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            FixupEntities();

            return base.SaveChangesAsync(cancellationToken);
        }

        private void FixupEntities()
        {
            var entries = ChangeTracker.Entries<IEntity>();

            foreach (var entry in entries)
            {
                _auditFieldFixer?.FixAuditFields(entry.Entity, GetEntityState(entry));
            }

            TruncateStringForChangedEntities(this);
        }

        private DbEntityState GetEntityState<T>(EntityEntry<T> entry)
            where T : class
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    return DbEntityState.Added;
                case EntityState.Modified:
                    return DbEntityState.Modified;
                default:
                    return DbEntityState.Unknown;
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(CompanyProfileDbContext)));
        }

        void IDbContext.Add(object entity)
        {
            base.Add(entity);
        }

        void IDbContext.Update(object entity)
        {
            base.Update(entity);
        }

        void IDbContext.Remove(object entity)
        {
            base.Remove(entity);
        }

        public IQueryable<T> Query<T>(Expression<Func<T, bool>> filter)
            where T : class, IEntity
        {
            return base.Set<T>().Where(filter).AsQueryable();
        }

        public string GetTableName<T>()
            where T : class, IEntity
        {
            return base.Model.FindEntityType(typeof(T)).GetTableName();
        }

        public void Execute(FormattableString sql)
        {
            //not supported by in-memory database
            if (!IsInMemoryDb)
            {
                var rawSql = string.Format(sql.Format, sql.GetArguments());
                base.Database.ExecuteSqlRaw(rawSql);
            }
            //base.Database.ExecuteSqlInterpolated(sql); //throws some @p0 exception - DJL 2/4/2020
        }
        
        //https://devhow.net/2019/01/17/entity-framework-core-truncating-strings-based-on-length-constraint/
        public static void TruncateStringForChangedEntities(DbContext context)
        {
            var stringPropertiesWithLengthLimitations = context.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string))
                .Select(z => new
                {
                    StringLength = z.GetMaxLength(),
                    ParentName = z.DeclaringEntityType.Name,
                    PropertyName = z.Name
                })
                .Where(d => d.StringLength.HasValue);


            var editedEntitiesInTheDbContextGraph = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(x => x.Entity);


            foreach (var entity in editedEntitiesInTheDbContextGraph)
            {
                var entityFields = stringPropertiesWithLengthLimitations.Where(d => d.ParentName == entity.GetType().FullName);

                foreach (var property in entityFields)
                {
                    var prop = entity.GetType().GetProperty(property.PropertyName);

                    if (prop == null)
                        continue;

                    var originalValue = prop.GetValue(entity) as string;
                    if (originalValue == null)
                        continue;

                    if (originalValue.Length > property.StringLength)
                    {
                        var entityTyped = entity as IEntity;
                        _log.Debug($"Entity '{entity.GetType().Name}':{entityTyped?.Id} Had value truncated from {originalValue.Length} to {property.StringLength} on property '{property.PropertyName}'");
                        prop.SetValue(entity, originalValue.Substring(0, property.StringLength.Value));
                    }
                }
            }
        }
    }    
}
