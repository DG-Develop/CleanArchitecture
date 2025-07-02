using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Commands;
using ECommerce.Domain.EcommerceDbEntities;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, int>
    {
        private readonly IMapper _mapper;

        public CreateSaleCommandHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<int> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {

            var newSale = _mapper.Map<Sale>(request);

            // Add Async

            throw new NotImplementedException();
        }
    }
}
