using ECommerce.Application.Features.Products.DTOS;
using ECommerce.Domain.EcommerceDbEntities;
using MediatR;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetPaginationProductQuery : IRequest<List<PaginationProductsDTO>>
    {
        public int TotalPage { get; set; }
        public int NumPage { get; set; } //requested page number
        public int? FilterProductType { get; set; } //Any filter for apply
        public int Take { get; set; } //Any filter for apply
    }
}
