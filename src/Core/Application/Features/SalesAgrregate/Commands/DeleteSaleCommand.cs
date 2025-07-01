using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Commands
{
    public record DeleteSaleCommand(int SaleId) : IRequest<Unit>;
}
