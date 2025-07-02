using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class DeleteSaleCommandHandler : IRequestHandler<DeleteSaleCommand, Unit>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleDetailsRepository _saleDetailsRepository;
        

        public DeleteSaleCommandHandler(ISaleRepository saleRepository, ISaleDetailsRepository saleDetailsRepository )
        {
            _saleRepository = saleRepository;
            _saleDetailsRepository = saleDetailsRepository;
        }

        public async Task<Unit> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            await _saleRepository.DeleteLogicSale(request.SaleId);



            //// Eliminación lógica de la venta
            //var sale = await _saleRepository.GetById(request.SaleId);
            //sale.Active = false;


            //// Eliminación lógica de los detalles de la venta
            //var listSaleDetails = _saleDetailsRepository.GetByFilter(x => x.IdSale == request.SaleId).ToList();

            //await _saleDetailsRepository.DeleteLogicSaleDetailsAsync( listSaleDetails.Select(x => x.IdSaleDetails).ToList());
            return Unit.Value;
        }
    }
}
