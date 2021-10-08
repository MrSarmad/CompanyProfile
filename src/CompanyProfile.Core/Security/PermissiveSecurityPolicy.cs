using ASI.Services.Access.Search;
using CompanyProfile.Core.Models;
using System.Linq;

namespace CompanyProfile.Core.Security
{
    public sealed class PermissiveSecurityPolicy : ISecurityPolicy
    {
        public IQueryable<T> Filter<T>(IQueryable<T> query)
            where T : class, IEntity
        {
            return query;
        }

        public IQueryable<T> FilterTenant<T>(IQueryable<T> query)
            where T : TenantEntity
        {
            return query;
        }

        public void FixupEntity<T>(T entity)
            where T : TenantEntity
        {

        }

        public ISearchSecurityFilter<T> GetSearchFilter<T>()
        {
            return new PermissiveSearchFilter<T>();
        }
    }
}
