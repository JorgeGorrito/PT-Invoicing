using AutoMapper;
using Invoicing.Application.Invoices.Queries.GetInvoicesList;
using Invoicing.Application.Invoices.Queries.GetInvoiceById;
using Invoicing.Domain.Entities;
using Invoicing.Domain.ValueObjects;

namespace Invoicing.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
       // Mapeo para listado de facturas (resumen)
       CreateMap<Invoice, InvoiceDTO>()
            .ForMember(dest => dest.ClientName,
                opt => opt.MapFrom(src => $"{src.Client.FirstName} {src.Client.LastName}"))
            .ForMember(dest => dest.ClientDocNumber,
                opt => opt.MapFrom(src => src.Client.DocNumber))
            .ForMember(dest => dest.Total,
                opt => opt.MapFrom(src => src.TotalValue));

       // Mapeo para detalle de factura (completo con items)
       CreateMap<Invoice, InvoiceDetailDTO>()
            .ForMember(dest => dest.Client, opt => opt.MapFrom(src => src.Client))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

       CreateMap<ClientInfo, ClientInfoDTO>();

       CreateMap<InvoiceItem, InvoiceItemDetailDTO>();
    }
}

