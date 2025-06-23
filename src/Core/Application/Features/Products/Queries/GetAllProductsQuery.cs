using MediatR;
using ECommerce.Domain;
using System.Collections.Generic;
using ECommerce.Domain.EcommerceDbEntities;

namespace ECommerce.Application.Features.Products.Queries
{
    public class GetAllProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
