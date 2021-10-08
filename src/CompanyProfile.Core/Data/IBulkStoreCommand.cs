using System;
using System.Linq.Expressions;

namespace CompanyProfile.Core.Data
{
    public interface IBulkStoreCommand : IStoreCommand
    {

    }

    public interface IBulkStoreCommand<T> : IBulkStoreCommand
    {
        Expression<Func<T, bool>> Selector { get; }
    }
}
