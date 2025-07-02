using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        public Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var d = request.Details;
            //Obtener los detalles de la venta filtrado por IdSale IdProduct
            // buscar por idSale 
            IdSaleDetails IdSale  IdProduct Amount  PriceOrigin Total   Active
                        1   1   1   2         899.99  1799.98 1
                        2   2   2   2         39.95   79.90   1
                     -- 3   2   3   1         125.50  125.50  1


            // Actualizar detalles de la venta


            // Obtener la venta por SaleId para actualizar MontoTotal 
            // context.Sale -> SaleDetails
            // context.Sale.SaleDetails = request.Details // simula que se hizo el mapper


        // Actualizar los detalles de la venta ya existentes o agregar nuevos detalles

        new Sale().SaleDetails.Add();

            // Guardar los cambios en la base de datos

            return Task.FromResult(Unit.Value);
        }
    }
}
