using AutoMapper;
using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using ECommerce.Application.Features.SalesAgrregate.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, IdDetailSalesDto>
    {
        //inyeccion
        private readonly IMapper _mapper;
        private readonly ISalesRepository _salesRepository;

        public  GetSaleByIdQueryHandler(IMapper mapper, ISalesRepository salesRepository) 
        {
            _mapper = mapper;
            _salesRepository = salesRepository;

        }



        public async Task<IdDetailSalesDto> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var query =  _salesRepository.GetByFilterInclude(x => x.IdSale == request.SaleId, x => x.SaleDetails);
             Sale? sale = await query.FirstOrDefaultAsync();

            if (sale == null)
            {
                throw new KeyNotFoundException($"Sale with ID {request.SaleId} not found.");
            }

            var salesMapped = _mapper.Map<IdDetailSalesDto>(sale);

            return salesMapped;
        }
    }
}
