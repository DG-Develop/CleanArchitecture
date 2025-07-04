using ECommerce.Application.Features.SalesAgrregate.Dtos.Request;
using ECommerce.Application.Features.SalesAgrregate.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.API.Sales
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {

        private readonly IMediator _mediator;


        public SalesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //localhost:5000/api/sales
        [HttpGet]
        public async Task<IActionResult> GetSalesAsync()
        {
            var response = await _mediator.Send(new GetAllSalesQuery());
            return Ok(new { Message = "Lista de ventas", Data = response });
        }

        //localhost:5000/api/sales/1
        [HttpGet("{salesId}")]
        //public async Task<IActionResult> GetSaleByIdAsync([FromRoute] int salesId, [FromQuery] SalesParametersDto salesParametersDto)
        public async Task<IActionResult> GetSaleByIdAsync([FromRoute] int salesId)
        {
            var response = await _mediator.Send(new GetSaleByIdQuery(salesId));

            return Ok(new { Message = "Detalle de venta", Data = response });
        }
    }
}
