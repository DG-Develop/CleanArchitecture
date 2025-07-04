using ECommerce.Application.Features.SalesAgrregate.Commands;
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

        [HttpPost]
        //public async Task<IActionResult> CreateSalesAsync([FromForm] SalesFormDto salesFormDto)
        //TODO: Implements FluentValidation for Command
        public async Task<IActionResult> CreateSalesAsync([FromBody] CreateSalesRequestDto body)
        {
            if (body == null)
            {
                return BadRequest(new { Message = "El cuerpo de la solicitud no puede estar vacío." });
            }
            var response = await _mediator.Send(new CreateSalesCommand(body.Amount, body.Total, body.PaymentTypeId));
            return Created($"/api/sales/{response}", response);
        }


        // 
        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateSalesAsync([FromRoute] int saleId, [FromBody] List<UpdateSaleRequestDto> updateSaleRequestList)
        {
            if(updateSaleRequestList == null || !updateSaleRequestList.Any())
            {
                return BadRequest(new { Message = "La lista de actualizaciones no puede estar vacía." });
            }

            await _mediator.Send(new UpdateSalesCommand(saleId, updateSaleRequestList));

            return NoContent();
        }
    }
}
