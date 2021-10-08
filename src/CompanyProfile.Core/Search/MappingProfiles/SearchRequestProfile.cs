using ASI.Contracts.CompanyProfile;
using ASI.Contracts.CompanyProfile.Search;
using ASI.Services.Search.Models;
using AutoMapper;

namespace CompanyProfile.Core.Search.MappingProfiles
{
    public sealed class SearchRequestProfile : Profile
    {
        public SearchRequestProfile()
        {
            CreateMap<SearchRequest, SearchCriteriaView>()
                .ReverseMap()
                ;
        }
    }
}
