using ASI.Contracts.CompanyProfile;
using ASI.Sugar.Collections;
using AutoMapper;
using CompanyProfile.Core.Models;
using System.Linq;

namespace CompanyProfile.Core.MappingProfiles
{
    public sealed class MyEntityProfile : Profile
    {
        public MyEntityProfile()
        {
            CreateMap<MyEntity, MyEntityView>()
                .ForMember(x => x.PrimaryAddress, c => c.MapFrom(y => y.Addresses.OrEmptyIfNull().FirstOrDefault(x => x.IsPrimary)))
                .ReverseMap()
                .IgnoreSourceValidation(x => x.PrimaryAddress!)
                ;
        }
    }

    public sealed class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressView>()
                .ReverseMap()
                ;
        }
    }
}
