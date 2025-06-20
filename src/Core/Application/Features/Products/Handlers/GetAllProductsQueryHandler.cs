using MediatR;
using ECommerce.Domain.Features.Products;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Features.Products.Queries;

namespace ECommerce.Application.Features.Products.Handlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<Product>>
    {
        private readonly IProductRepository _repository;

        public GetAllProductsQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
