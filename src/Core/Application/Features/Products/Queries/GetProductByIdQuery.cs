using MediatR;
using ECommerce.Domain.Features.Products;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Product?>
    {
        public int Id { get; set; }
    }
}
