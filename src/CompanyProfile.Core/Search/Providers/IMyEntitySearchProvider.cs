using ASI.Services.Search.Models;
using CompanyProfile.Core.Search.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProfile.Core.Search.Providers
{
    public interface IMyEntitySearchProvider
    {
        Task<SearchResult<SearchMyEntity>> SearchAsync(SearchRequest searchCriteria);
    }
}
