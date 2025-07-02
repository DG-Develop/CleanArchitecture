using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _SaleRepository;
        

        public CreateSaleCommandHandler(IMapper mapper, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _SaleRepository = saleRepository;
        }

        public async Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {

            var newSale = _mapper.Map<Sale>(request);

            Sale? saleSave = await _SaleRepository.AddAsync(newSale);

            if(saleSave == null)
            {
                throw new Exception("Error al crear la venta");
            }

            return saleSave.IdSale;
        }
    }
}
