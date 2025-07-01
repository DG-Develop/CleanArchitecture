using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Commands
{
    public record CreateSaleCommand(int Amount, decimal Total, int PaymentTypeId) : IRequest<int>;
}
