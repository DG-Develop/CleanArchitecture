using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using ECommerce.Application.Features.SalesAgrregate.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.EcommerceDbEntities;
using AutoMapper;



namespace ECommerce.Application.Features.SalesAgrregate.Handlers
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<SalesDto>>
    {
        private readonly IGenericRepository<Sale> _repo;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(IGenericRepository<Sale> repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<SalesDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            //IdPaymentTypeNavigation
            // Implements to get data from repository
            IQueryable<Sale> response = _repo.GetByFilterInclude(x => x.Active == true, include => include.IdPaymentTypeNavigation);
            var listproduct = response.ToList();

            var responseDto = _mapper.Map<List<SalesDto>>(listproduct);
            return responseDto;
            /*
             * --Scaffold (DB firts)
             * --Migragration (Code first)
             */



        }
    }
}
