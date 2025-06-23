using MediatR;
using ECommerce.Domain.EcommerceDbEntities;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Features.Products.Commands;
using ECommerce.Domain.Interfaces;

namespace ECommerce.Application.Features.Products.Handlers
{
  public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
  {
    private readonly IProductRepository _repository;

    public UpdateProductCommandHandler(IProductRepository repository)
    {
      _repository = repository;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
      var product = await _repository.GetByIdAsync(request.Id);
      if (product is null)
      {
        throw new KeyNotFoundException($"Product with Id {request.Id} not found.");
      }

        product.Name = request.Name;
        product.Description = request.Description;  
        product.Price = request.Price;
      await _repository.UpdateAsync(product);
      return Unit.Value;
    }
  }
}
