using MediatR;
using ECommerce.Domain.Features.Products;
using System.Threading;
using System.Threading.Tasks;
using ECommerce.Application.Features.Products.Commands;

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

      product.Update(request.Name, request.Description, request.Price);
      await _repository.UpdateAsync(product);
      return Unit.Value;
    }
  }
}
