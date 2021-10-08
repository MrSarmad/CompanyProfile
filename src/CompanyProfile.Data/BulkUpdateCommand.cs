using CompanyProfile.Core.Data;
using CompanyProfile.Core.Models;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace CompanyProfile.Data
{
    public sealed class BulkUpdateCommand<TModel> : IBulkStoreCommand<TModel>
        where TModel : class, IEntity
    {
        public StoreInteractionType Type => StoreInteractionType.Updated;
        public Expression<Func<TModel, bool>> Selector { get; }
        public Expression<Func<TModel, object>> UpdateExpression { get; }

        public BulkUpdateCommand(Expression<Func<TModel, bool>> selector, Expression<Func<TModel, object>> updateExpression)
        {
            Selector = selector ?? throw new ArgumentNullException(nameof(selector));
            UpdateExpression = updateExpression ?? throw new ArgumentNullException(nameof(updateExpression));
        }

        public void Execute(IDbContext context)
        {
            if (context.SupportsBulk())
            {
                context.Query(Selector).UpdateFromQuery(UpdateExpression);
            }
            else
            {
                var entities = context.Query(Selector).ToList();
                foreach (var e in entities)
                {
                    UpdateExpression.Compile()(e);
                    context.Update(e);
                }
            }
        }
    }
}
