using AutoMapper;
using ECommerce.Application.Features.Products.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using ECommerce.Domain.ValueObject.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Handlers
{
    public class GetProductFilterQueryHandle: IRequestHandler<GetProductFilterQuery, List<ProductResultDTO>?> 
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductFilterQueryHandle(IProductRepository repository, IMapper iMapper)
        {
            _repository = repository;
            _mapper = iMapper;
        }


        public async Task<List<ProductResultDTO>?> Handle(GetProductFilterQuery request, CancellationToken cancellationToken)
        {
            // Get products based on the filter criteria
            ProductFilterValue filterValue =  _mapper.Map<ProductFilterValue>(request);
            List<ProductResultDTO> listProducts = await _repository.GetProductsFilterAsync(filterValue);
            
            // Return the filtered product list
            return listProducts;
        }

    }
}
