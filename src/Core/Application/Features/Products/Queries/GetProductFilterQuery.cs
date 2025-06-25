using ECommerce.Application.Features.Products.DTOS;
using ECommerce.Domain.ValueObject.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.Products.Queries
{
    public record GetProductFilterQuery(GetProductParametersDTO query) : IRequest<List<ProductResultDTO>?>;
    
}
