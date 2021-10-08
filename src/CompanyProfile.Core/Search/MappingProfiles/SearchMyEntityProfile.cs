using AutoMapper;
using CompanyProfile.Core.Models;
using CompanyProfile.Core.Search.Models;

namespace CompanyProfile.Core.Search.MappingProfiles
{
    /// <summary>
    /// Mapper from Business Model / DB Model -> Search Model
    /// </summary>
    public class SearchMyEntityProfile : Profile
    {
        public SearchMyEntityProfile()
        {
            CreateMap<MyEntity, SearchMyEntity>()
                ;
        }
    }
}
