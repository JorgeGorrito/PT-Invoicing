using AutoMapper;
using Invoicing.Domain.ValueObjects;
using Invoicing.Infrastructure.ExternalModels;

namespace Invoicing.Infrastructure.Common.Mappings;

public class NovasoftMappingProfile : Profile
{
    public NovasoftMappingProfile()
    {
        CreateMap<NovasoftAccountResponse, ExternalAccount>()
            .ForMember(dest => dest.ClientCode, opt => opt.MapFrom(src => src.ClientCode))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Identification, opt => opt.MapFrom(src => src.Identification))
            .ForMember(dest => dest.CityCode, opt => opt.MapFrom(src => src.CityCode))
            .ForMember(dest => dest.StateCode, opt => opt.MapFrom(src => src.StateCode))
            .ForMember(dest => dest.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? string.Empty))
            .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone ?? string.Empty))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.ClientType, opt => opt.MapFrom(src => src.ClientType))
            .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate))
            .ForMember(dest => dest.IdentificationType, opt => opt.MapFrom(src => "06"))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName ?? string.Empty))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName ?? string.Empty))
            .ForMember(dest => dest.PersonType, opt => opt.MapFrom(src => 2))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => "1"))
            .ForMember(dest => dest.ExternalClientCode, opt => opt.MapFrom(src => (string?)null))
            .ForMember(dest => dest.WebPage, opt => opt.MapFrom(src => (string?)null));

        CreateMap<ExternalAccount, NovasoftAccountRequest>()
            .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToString("yyyy-MM-dd")));
    }
}
