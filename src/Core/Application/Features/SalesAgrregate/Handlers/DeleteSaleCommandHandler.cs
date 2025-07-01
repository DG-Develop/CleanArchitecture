using ECommerce.Application.Features.SalesAgrregate.Commands;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit>
    {
        public Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            // Eliminación lógica de la venta

            // Eliminación lógica de los detalles de la venta

            return Task.FromResult(Unit.Value);
        }
    }
}
