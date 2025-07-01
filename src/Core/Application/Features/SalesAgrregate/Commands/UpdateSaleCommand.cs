using ECommerce.Application.Features.SalesAgrregate.Dtos.Request;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Commands
{
    public record UpdateSaleCommand(int SaleId, List<UpdateSaleRequestDto> Details) : IRequest<Unit>;
}
