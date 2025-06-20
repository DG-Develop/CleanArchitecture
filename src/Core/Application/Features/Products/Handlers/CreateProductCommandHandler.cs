using MediatR;
using ECommerce.Domain.Features.Products;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Features.Products.Commands;

namespace ECommerce.Application.Features.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product(0, request.Name, request.Description, request.Price);
            return await _repository.AddAsync(product);
        }
    }
}
