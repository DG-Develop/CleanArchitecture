using MediatR;
using ECommerce.Domain.EcommerceDbEntities;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Features.Products.Commands;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Features.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
    {
        private readonly IGenericRepository<Product> _genericRepository;
        private readonly ICatProductsType _catProductsType;

        public CreateProductCommandHandler(IGenericRepository<Product> repository)
        {
            _genericRepository = repository;
        }

        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product {Name = request.Name, Description = request.Description, Price= request.Price };
            Product productAdded = await _genericRepository.AddAsync(product);

            //_catProductsType.GetReport();
            //_catProductsType.AddAsync();
            //_catProductsType.UpdateAsync();
            //jo
            return productAdded.Id;
        }
    }
}
