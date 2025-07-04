using ECommerce.Application.Features.SalesAgrregate.Dtos.Request;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Commands
{
    public record UpdateSalesCommand(int SaleId, List<UpdateSaleRequestDto> UpdateSaleRequestList) : IRequest<Unit>;
}
