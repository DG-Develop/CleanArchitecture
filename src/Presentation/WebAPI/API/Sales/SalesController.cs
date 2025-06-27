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
    }
}
