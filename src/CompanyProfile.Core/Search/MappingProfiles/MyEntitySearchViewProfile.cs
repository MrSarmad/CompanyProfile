using ASI.Contracts.CompanyProfile.Search;
using AutoMapper;
using CompanyProfile.Core.Search.Models;

namespace CompanyProfile.Core.Search.MappingProfiles
{
    /// <summary>
    /// Mapper from the Search Model -> Search View to be returned by the API
    /// </summary>
    public class MyEntitySearchViewProfile : Profile
    {
        public MyEntitySearchViewProfile()
        {
            CreateMap<SearchMyEntity, MyEntitySearchView>()
                ;
        }
    }
}
