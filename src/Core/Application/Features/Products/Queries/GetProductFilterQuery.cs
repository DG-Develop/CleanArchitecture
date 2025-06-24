using ECommerce.Domain.ValueObject.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetProductFilterQuery : IRequest<List<ProductResultDTO>?>
    {
        public string? Name { get; set; }
        public int? ProductTypeId { get; set; }
        public int? CoinId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }

        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
