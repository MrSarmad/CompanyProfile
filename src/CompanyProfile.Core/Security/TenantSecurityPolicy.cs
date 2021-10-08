using ASI.Services.Access.Search;
using CompanyProfile.Core.Context;
using CompanyProfile.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyProfile.Core.Security
{
    public sealed class TenantSecurityPolicy : ISecurityPolicy
    {
        private readonly ITenantContext _tenantContext;
        private readonly IEnumerable<ISearchSecurityFilter> _searchSecurityFilters;

        public TenantSecurityPolicy(ITenantContext tenantContext, IEnumerable<ISearchSecurityFilter> searchSecurityFilters)
        {
            _tenantContext = tenantContext ?? throw new ArgumentNullException(nameof(tenantContext));
            _searchSecurityFilters = searchSecurityFilters ?? throw new ArgumentNullException(nameof(searchSecurityFilters));
        }

        public IQueryable<T> Filter<T>(IQueryable<T> query)
            where T : class, IEntity
        {
            return query;
        }

        public IQueryable<T> FilterTenant<T>(IQueryable<T> query)
            where T : TenantEntity
        {
            return query.Where(x => x.TenantId == _tenantContext.TenantId);
        }

        public void FixupEntity<T>(T entity)
            where T : TenantEntity
        {
            entity.TenantId = _tenantContext.TenantId;
        }

        public ISearchSecurityFilter<T> GetSearchFilter<T>()
        {
            var filter = _searchSecurityFilters.OfType<ISearchSecurityFilter<T>>().SingleOrDefault();
            return filter;
        }
    }
}
