using ASI.Services.Access.Search;
using CompanyProfile.Core.Models;
using System.Linq;

namespace CompanyProfile.Core.Security
{
    public interface ISecurityPolicy
    {
        IQueryable<T> Filter<T>(IQueryable<T> query) where T : class, IEntity;
        IQueryable<T> FilterTenant<T>(IQueryable<T> query) where T : TenantEntity;
        void FixupEntity<T>(T entity) where T : TenantEntity;
        ISearchSecurityFilter<T> GetSearchFilter<T>();
    }       
}
