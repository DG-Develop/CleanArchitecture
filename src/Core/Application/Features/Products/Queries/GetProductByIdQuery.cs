using MediatR;
using ECommerce.Application.DTOs.Products;
using ECommerce.Domain.EcommerceDbEntities;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<PaginationProductsDTO?>
    {
        public int Id { get; set; }
    }
}
