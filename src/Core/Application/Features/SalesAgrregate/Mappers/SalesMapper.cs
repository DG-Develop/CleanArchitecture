using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
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

            CreateMap<CreateSalesCommand, Sale>()
                .ForMember(dest => dest.IdPaymentType, origen => origen.MapFrom(o => o.PaymentTypeId))
                .ForMember(dest => dest.Date, origen => origen.MapFrom(o => DateTime.Now))
                .ForMember(dest => dest.Active, origen => origen.MapFrom(o => true));
        }
    }
}
