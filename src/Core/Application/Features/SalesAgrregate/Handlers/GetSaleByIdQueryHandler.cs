using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using ECommerce.Application.Features.SalesAgrregate.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, IdDetailSalesDto>
    {
        
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<IdDetailSalesDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            Sale? sale = await _saleRepository.GetById(request.IdSale);

            if(sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {request.IdSale} not found.");
            }

            var saleMapped = _mapper.Map<IdDetailSalesDto>(sale);

            return saleMapped;
        }
    }
}
