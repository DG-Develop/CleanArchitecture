using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class CreateDetailSaleCommandHandler : IRequestHandler<CreateDetailSaleCommand, int>
    {
        
        private readonly IProductRepository _productRepository;
        private readonly ISaleDetailsRepository _saleDetailsRepository;
        private readonly IMapper _mapper;

        public CreateDetailSaleCommandHandler(IProductRepository productRepository, ISaleDetailsRepository saleDetailsRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _saleDetailsRepository = saleDetailsRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateDetailSaleCommand request, CancellationToken cancellationToken)
        {
            // Obtener precio del  Producto por ProductId
            var product = await _productRepository.GetById(request.ProductId);


            // Calcular Precio Total (Precio * Cantidad)
            var total = request.Amount * product.Price;

            var detailSale = _mapper.Map<SaleDetail>(request);
            detailSale.Total = total;
            detailSale.PriceOrigin = product.Price;

            //AddASync
            var detailSaleSave = await _saleDetailsRepository.AddAsync(detailSale);
            return detailSaleSave.IdSaleDetails;
        }
    }
}
