using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Queries
{
    public record GetSaleByIdQuery(int SaleId) : IRequest<IdDetailSalesDto>;
    
}
