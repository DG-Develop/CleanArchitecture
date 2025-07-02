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
        private readonly ISaleDetailsRepository _SaleDetailsRepository;
        private readonly IGenericRepository<Sale> _repo;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(IGenericRepository<Sale> repo, IMapper mapper, ISaleDetailsRepository SaleDetailsRepository)
        {
            _repo = repo;
            _mapper = mapper;
            _SaleDetailsRepository = SaleDetailsRepository;
        }

        public async Task<List<SalesDto>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            List<int> lista = new List<int> { 2, 3 }; 
            List<SaleDetail> listsaleDetail = new List<SaleDetail>();
            listsaleDetail = await _SaleDetailsRepository.GetSaleDetailsByIdVentaListIdProducts(2, lista, false);

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
