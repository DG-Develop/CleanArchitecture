using MediatR;
using ECommerce.Domain.Features.Products;
using System.Collections.Generic;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
