using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class UpdateSalesCommandHandler : IRequestHandler<UpdateSalesCommand, Unit>
    {
        private readonly ISalesRepository _salesRepository;

        public async Task<Unit> Handle(UpdateSalesCommand request, CancellationToken cancellationToken)
        {
            Sale sale = await _salesRepository.GetSaleDetails(request.SaleId);

            request.UpdateSaleRequestList.Select(d =>
            {
                var newSaleDetail = sale.SaleDetails.FirstOrDefault(x => x.IdProduct == d.ProductId && x.Active);
                    if (newSaleDetail != null)
                    {
                        newSaleDetail.Amount = d.Amount;
                        newSaleDetail.Total = d.Amount * newSaleDetail.PriceOrigin;
                    }

                return d.ProductId;
            }).Where(id => !sale.SaleDetails.Any(x => x.IdProduct == id))
             .ToList();




            /*
             * buscar el producto ? precioORigen
             * revisar en el contexto que se agregue el nuevo producto con su precio, cantidad
             * 
             * actualizar el total de la venta 
             */






            ///
            //idventa = 2, total = -+

            /*
            x => 2 = 10
            //y = 3 = 15
            y => 10 = 50

            //addd
            z => 2 = 20
            */


            throw new NotImplementedException();
        }
    }
}
