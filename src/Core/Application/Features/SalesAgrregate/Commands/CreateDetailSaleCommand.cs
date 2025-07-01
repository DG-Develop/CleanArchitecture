using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Commands
{
    public record CreateDetailSaleCommand(int SaleId, int ProductId, int Amount) : IRequest<int>;
}
