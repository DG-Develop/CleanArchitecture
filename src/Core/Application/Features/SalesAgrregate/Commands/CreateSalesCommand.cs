using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Commands
{
    public record CreateSalesCommand(int Amount, decimal Total, int PaymentTypeId) : IRequest<int>;
}
