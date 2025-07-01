using ECommerce.Application.Features.SalesAgrregate.Commands;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        public Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            // Actualizar detalles de la venta
            // Obtener la venta por SaleId
            // Actualizar los detalles de la venta ya existentes o agregar nuevos detalles

            return Task.FromResult(Unit.Value);
        }
    }
}
