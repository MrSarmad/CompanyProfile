using Microsoft.EntityFrameworkCore;
using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.Security;
using CompanyProfile.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompanyProfile.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly CompanyProfileDbContext _dbContext;
        private readonly ISecurityPolicy _securityPolicy;

        public DataAccess(CompanyProfileDbContext dbContext, ISecurityPolicy securityPolicy)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _securityPolicy = securityPolicy ?? throw new ArgumentNullException(nameof(securityPolicy));            
        }

        public IDataTransaction CreateTransaction()
        {
            return new DataTransaction(this);
        }

        private IQueryable<T> Query_Internal<T>() where T : class, IEntity
        {
            var q = _dbContext.Set<T>().AsQueryable();

            q = q.Where(x => !x.IsDeleted);

            return q;
        }

        private IQueryable<T> Query_Internal<T>(params Expression<Func<T, object>>[] includes) where T : class, IEntity
        {
            var q = Query_Internal<T>();

            foreach (var i in includes)
                q = q.Include(i);

            return q;
        }

        public IQueryable<T> Query<T>() where T : class, IEntity
        {
            return _securityPolicy.Filter(Query_Internal<T>());
        }

        public IQueryable<T> QueryTenant<T>() where T : TenantEntity
        {
            return _securityPolicy.FilterTenant(Query_Internal<T>());
        }

        public IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includes) where T : class, IEntity
        {
            return _securityPolicy.Filter(Query_Internal(includes));
        }

        public IQueryable<T> QueryTenant<T>(params Expression<Func<T, object>>[] includes) where T : TenantEntity
        {
            return _securityPolicy.FilterTenant(Query_Internal(includes));
        }

        public IQueryable<T> QueryTenantNoTracking<T>(params Expression<Func<T, object>>[] includes) where T : TenantEntity
        {
            return _securityPolicy.FilterTenant(Query_Internal(includes).AsNoTracking());
        }

        public IQueryable<T> Include<T, TP1>(IQueryable<T> query, Expression<Func<T, TP1>> expr1) where T : class
        {
            return query.Include(expr1);
        }

        public IQueryable<T> Include<T, TP1, TP2>(IQueryable<T> query, Expression<Func<T, TP1>> expr1, Expression<Func<TP1, TP2>> expr2) where T : class
        {
            return query.Include(expr1).ThenInclude(expr2);
        }

        public IQueryable<T> Include<T, TP1, TP2>(IQueryable<T> query, Expression<Func<T, ICollection<TP1>>> expr1, Expression<Func<TP1, TP2>> expr2) where T : class
        {
            return query.Include(expr1).ThenInclude(expr2);
        }

        public void ExecuteCommands(IReadOnlyList<IStoreCommand> commands)
        {
            Execute_Internal(commands);

            _dbContext.SaveChanges();
        }

        public Task ExecuteCommandsAsync(IReadOnlyList<IStoreCommand> commands)
        {
            Execute_Internal(commands);

            return _dbContext.SaveChangesAsync();
        }

        private void Execute_Internal(IReadOnlyList<IStoreCommand> commands)
        {
            foreach (var command in commands)
            {
                switch (command)
                {
                    case IEntityStoreCommand entityCmd:
                        if (entityCmd.Entity is TenantEntity t)
                            _securityPolicy.FixupEntity(t);

                        break;
                }

                command.Execute(_dbContext);
            }
        }
    }
}
