using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using ECommerce.Domain.EcommerceDbEntities;

namespace ECommerce.Application.Features.SalesAgrregate.Mappers
{
    public class SalesMapper : Profile
    {
        public SalesMapper() 
        {
            CreateMap<Sale, SalesDto>()
                .ForMember(dest => dest.SaleId, origen => origen.MapFrom(o => o.IdSale))
                .ForMember(dest => dest.DescriptionPayment, origen => origen.MapFrom(o => o.IdPaymentTypeNavigation.Description))
                .ForMember(dest => dest.Date, origen => origen.MapFrom(o => o.Date.ToShortTimeString()));
        }
    }
}
