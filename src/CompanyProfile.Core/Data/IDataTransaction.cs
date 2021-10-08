using CompanyProfile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CompanyProfile.Core.Data
{
    public interface IDataTransaction
    {
        void Add<T>(T entity) where T : IEntity;
        void Update<T>(T entity) where T : IEntity;
        void Remove<T>(T entity) where T : IEntity;
        void BulkUpdate<T>(Expression<Func<T, bool>> selector, Expression<Func<T, object>> obj) where T : class, IEntity;
        void SoftDelete<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
        void BulkRemove<T>(Expression<Func<T, bool>> selector) where T : class, IEntity;
        void Commit();
        Task CommitAsync();
        IReadOnlyList<IStoreCommand> Commands { get; }
    } 
}
