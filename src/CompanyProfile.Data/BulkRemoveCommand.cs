using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompanyProfile.Data
{
    public sealed class BulkRemoveCommand<TModel> : IBulkStoreCommand<TModel>
        where TModel : class, IEntity
    {
        public StoreInteractionType Type => StoreInteractionType.Removed;
        public Expression<Func<TModel, bool>> Selector { get; }

        public BulkRemoveCommand(Expression<Func<TModel, bool>> selector)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
        }

        public void Execute(IDbContext context)
        {
            if (context.SupportsBulk())
            {
                context.Query(Selector).DeleteFromQuery();
            }
            else
            {
                // should only be needed for in memory integration tests, but causes the test not to fail.
                var toDelete = context.Query(Selector).ToList();
                foreach (var e in toDelete)
                    context.Remove(e);
            }
        }
    }
}
