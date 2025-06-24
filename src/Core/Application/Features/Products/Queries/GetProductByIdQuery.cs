using MediatR;
using ECommerce.Domain.EcommerceDbEntities;
using ECommerce.Application.Features.Products.DTOS;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<PaginationProductsDTO?>
    {
        public int Id { get; set; }
    }
}
