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


        [HttpGet]
        public async Task<IActionResult> GetSalesAsync()
        {

            var response = await _mediator.Send(new GetAllSalesQuery());
           
            return Ok(new { Message = "Lista de ventas", Data = response });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleByIdAsync(int id)
        {

            var response = await _mediator.Send(new GetSaleByIdQuery(id));
            return Ok(new { Message = "Venta encontrada", Data = response });
        }

        [HttpPost]
        public async Task<IActionResult> CreateSaleAsync([FromBody] CreateSaleRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Datos de venta inválidos" });
            }
            var response = await _mediator.Send(new CreateSaleCommand(request.Amount, request.Total, request.PaymentTypeId));

            return Created("", response);
        }

        [HttpPost("detail/{saleId}")]
        public async Task<IActionResult> CreateSaleDetailAsync(int saleId, [FromBody] CreateDetailSaleRequestDto request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Detalles de venta inválidos" });
            }
            
            var response = await _mediator.Send(new CreateDetailSaleCommand(saleId, request.ProductId, request.Amount)); 
            return Created("", response);
        }

        [HttpPut("{saleId}")]
        public async Task<IActionResult> UpdateSaleAsync(int saleId, [FromBody] List<UpdateSaleRequestDto> request)
        {
            if (request == null)
            {
                return BadRequest(new { Message = "Datos de venta inválidos" });
            }
            
            var response = await _mediator.Send(new UpdateSaleCommand(saleId, request));
            return NoContent();
        }

        [HttpDelete("{saleId}")]
        public async Task<IActionResult> DeleteSaleAsync(int saleId)
        {
            var response = await _mediator.Send(new DeleteSaleCommand(saleId));

            return NoContent();
        }
    }
}
