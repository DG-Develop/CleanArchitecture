using ECommerce.Application.Features.SalesAgrregate.Dtos.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Features.SalesAgrregate.Queries
{
    public record GetSaleByIdQuery(int IdSale) : IRequest<IdDetailSalesDto>;
}
