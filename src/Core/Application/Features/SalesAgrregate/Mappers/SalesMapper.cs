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

            CreateMap<Sale, IdDetailSalesDto>()
                .ForMember(dest => dest.SaleId, origen => origen.MapFrom(o => o.IdSale))
                .ForMember(dest => dest.DescriptionPayment, origen => origen.MapFrom(o => o.IdPaymentTypeNavigation.Description))
                .ForMember(dest => dest.Date, origen => origen.MapFrom(o => o.Date.ToShortTimeString()))
                .ForMember(dest => dest.DetailSales, origen => origen.MapFrom(o => o.SaleDetails));

            CreateMap<SaleDetail, DetailSalesDto>()
                .ForMember(dest => dest.ProductId, origen => origen.MapFrom(o => o.IdProductNavigation.IdProduct))
                .ForMember(dest => dest.OriginPrice, origen => origen.MapFrom(o => o.PriceOrigin));
        }
    }
}
