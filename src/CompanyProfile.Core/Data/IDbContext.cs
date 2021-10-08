using CompanyProfile.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompanyProfile.Core.Data
{
    public interface IDbContext
    {
        void Add(object entity);
        void Update(object entity);
        void Remove(object entity);
        IQueryable<T> Query<T>(Expression<Func<T, bool>> filter)
            where T : class, IEntity;
        void Execute(FormattableString sql);
        string GetTableName<T>()
            where T : class, IEntity;
        bool SupportsBulk();
    }
}
