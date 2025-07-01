using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using ECommerce.Application.Features.SalesAgrregate.Queries;
using MediatR;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, IdDetailSalesDto>
    {
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<IdDetailSalesDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {

            //var saleMapped = _mapper.Map<IdDetailSalesDto>(sale);

            //return saleMapped;

            throw new NotImplementedException();
        }
    }
}
