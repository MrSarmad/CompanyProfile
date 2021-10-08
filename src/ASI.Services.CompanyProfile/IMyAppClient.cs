using ASI.Contracts.CompanyProfile;
using ASI.Contracts.CompanyProfile.Search;
using System.Threading.Tasks;

namespace ASI.Services.CompanyProfile
{
    public interface ICompanyProfileClient
    {
        Task<MyEntityView> AddMyEntityAsync(MyEntityView view);
        Task<MyEntityView> GetMyEntityAsync(long id);
        Task<SearchResultView<MyEntitySearchView>> SearchAsync(SearchCriteriaView request);
    }
}
