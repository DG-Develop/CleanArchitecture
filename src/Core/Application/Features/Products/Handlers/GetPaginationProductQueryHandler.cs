using AutoMapper;
using ECommerce.Application.Features.Products.DTOS;
using ECommerce.Application.Features.Products.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Handlers
{
    public class GetPaginationProductQueryHandler : IRequestHandler<GetPaginationProductQuery, List<PaginationProductsDTO> >
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public GetPaginationProductQueryHandler(IMapper IMapper , IProductRepository IProductRepository)
        {
            _mapper = IMapper;
            _productRepository = IProductRepository;
        }

        public async Task<List<PaginationProductsDTO>> Handle(GetPaginationProductQuery request, CancellationToken cancellationToken)
        {
            List<Product> listProducts = await _productRepository.getPagination(request.NumPage, request.TotalPage, request.FilterProductType, request.Take);
            return _mapper.Map<List<PaginationProductsDTO>>(listProducts) ;
        }

    }
}
