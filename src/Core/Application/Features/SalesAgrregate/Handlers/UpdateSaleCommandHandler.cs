using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        public UpdateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository) 
        {
            _saleRepository = saleRepository;
            _productRepository  = productRepository;
        }

        public async Task<Unit> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            //Obtener los detalles de la venta filtrado por IdSale IdProduct
            // buscar por idSale 
            Sale sales = await _saleRepository.GetSaleAndDetails(request.SaleId);
            //      public int ProductId { get; set; }
            //public int Amount { get; set; }
            List<int> listIdProducts = new List<int>();
            foreach (var itemDetail in request.Details) 
            {
                var newSaleDetail = sales.SaleDetails.FirstOrDefault(x => x.IdProduct == itemDetail.ProductId);

                if (newSaleDetail != null) 
                {
                    // Actualizar el detalle existente
                    newSaleDetail.Amount = itemDetail.Amount;
                    newSaleDetail.Total = itemDetail.Amount * newSaleDetail.PriceOrigin; // Recalcular total
                }
                else 
                {
                    listIdProducts.Add(itemDetail.ProductId);
                    //// Agregar un nuevo detalle de venta
                    //sales.SaleDetails.Add(new SaleDetail 
                    //{ 
                    //    IdProduct = itemDetail.ProductId, 
                    //    Amount = itemDetail.Amount, 
                    //    PriceOrigin = itemDetail.PriceOrigin
                    //    Total = itemDetail.Amount * itemDetail.PriceOrigin, // Asumiendo que PriceOrigin está en el request
                    //});
                }

            }

            if (listIdProducts.Count > 0) 
            {
                //Add products that are not in the sale

                 var priceProduct = await _productRepository.GetByFilter(x => listIdProducts.Contains(x.IdProduct)).Select(x => new { x.IdProduct, x.Price }).ToListAsync();

                foreach (var itemPrice in priceProduct)
                {
                    var newSaleDetail = request.Details.FirstOrDefault(x => x.ProductId == itemPrice.IdProduct);

                    sales.SaleDetails.Add(new SaleDetail
                    {
                        IdSale = sales.IdSale,
                        IdProduct = newSaleDetail.ProductId,
                        Amount = newSaleDetail.Amount,
                        PriceOrigin = itemPrice.Price,
                        Total = itemPrice.Price * newSaleDetail.Amount,
                        Active = true
                    });
                }


            }


            //calculus total
            sales.Amount = sales.SaleDetails.Select(x => x.Amount).Sum();
            sales.Total = sales.SaleDetails.Sum(x => x.Total);

            await _saleRepository.saveChangeAsync();

            //request.Details
            // Actualizar detalles de la venta


            // Obtener la venta por SaleId para actualizar MontoTotal 
            // context.Sale -> SaleDetails
            // context.Sale.SaleDetails = request.Details // simula que se hizo el mapper


            // Actualizar los detalles de la venta ya existentes o agregar nuevos detalles

            // new Sale().SaleDetails.Add();

            // Guardar los cambios en la base de datos

            return Unit.Value;
        }
    }
}
