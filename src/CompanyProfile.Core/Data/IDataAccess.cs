using CompanyProfile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CompanyProfile.Core.Data
{
    public interface IDataAccess
    {
        IDataTransaction CreateTransaction();
        void ExecuteCommands(IReadOnlyList<IStoreCommand> commands);
        IQueryable<T> Include<T, TP1>(IQueryable<T> query, Expression<Func<T, TP1>> expr1) where T : class;
        IQueryable<T> Include<T, TP1, TP2>(IQueryable<T> query, Expression<Func<T, TP1>> expr1, Expression<Func<TP1, TP2>> expr2) where T : class;
        IQueryable<T> Include<T, TP1, TP2>(IQueryable<T> query, Expression<Func<T, ICollection<TP1>>> expr1, Expression<Func<TP1, TP2>> expr2) where T : class;
        IQueryable<T> Query<T>() where T : class, IEntity;
        IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includes) where T : class, IEntity;
        IQueryable<T> QueryTenant<T>() where T : TenantEntity;
        IQueryable<T> QueryTenant<T>(params Expression<Func<T, object>>[] includes) where T : TenantEntity;
        IQueryable<T> QueryTenantNoTracking<T>(params Expression<Func<T, object>>[] includes) where T : TenantEntity;
    }
}
