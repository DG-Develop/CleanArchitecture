using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class CreateSalesCommandHandler : IRequestHandler<CreateSalesCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesRepository;

        public CreateSalesCommandHandler(IMapper mapper, ISalesRepository salesRepository)
        {
            _mapper = mapper;
            _salesRepository = salesRepository;
        }

        public async Task<int> Handle(CreateSalesCommand request, CancellationToken cancellationToken)
        {
            var newSale = _mapper.Map<Sale>(request);

            Sale? saleSave =  await _salesRepository.AddAsync(newSale);

            if(saleSave == null)
            {
                throw new Exception("Error al crear la venta.");
            }

            return saleSave.IdSale;
        }
    }
}
