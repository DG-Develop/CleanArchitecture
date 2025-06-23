using AutoMapper;
using ECommerce.Application.DTOs.Products;
using ECommerce.Application.Features.Products.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Domain.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Handlers
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, PaginationProductsDTO?>
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly IMapper _mapper;


        public GetProductByIdQueryHandler( IGenericRepository<Product> iGenericRepository, IMapper iMapper)
        {
           
            _genericRepository = iGenericRepository;
            _mapper = iMapper;
        }

        public async Task<PaginationProductsDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            Product product = new Product();
            product = await _genericRepository.GetById(request.Id);
            return _mapper.Map<PaginationProductsDTO>(product);
        }
    }
}
