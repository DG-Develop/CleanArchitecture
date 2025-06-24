using Microsoft.AspNetCore.Mvc;
using MediatR;
using ECommerce.Application.Features.Products.Commands;
using ECommerce.Application.Features.Products.Queries;
using ECommerce.Domain.EcommerceDbEntities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.Application.Features.Products.DTOS;

namespace ECommerce.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("paginate-product")]
        public async Task<ActionResult<IEnumerable<Product>>> GetPagination([FromQuery] int NumPage, int TotalPage, int? FilterProductType = 1  /*Electrónicos */ , int take =10 )
        {
            var products = await _mediator.Send(new GetPaginationProductQuery { NumPage = NumPage, TotalPage = TotalPage, FilterProductType = FilterProductType , Take = take });
            return Ok(products);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("GetProductFilter")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductFilter([FromQuery] GetProductParametersDTO parametersDTO)
        {
            var products = await _mediator.Send(new GetProductFilterQuery(parametersDTO));
            return Ok(products);
        }


        #region CRUD
        
            //Repo Generico
            [HttpGet("{id}")]
            public async Task<ActionResult<Product>> GetById(int id)
            {
                var product = await _mediator.Send(new GetProductByIdQuery { Id = id });
                if (product is null)
                    return NotFound();
                return Ok(product);
            }

            
            [HttpPost]
            public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
            {
                var id = await _mediator.Send(command);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
            {
                if (id != command.Id)
                    return BadRequest();

                await _mediator.Send(command);
                return NoContent();
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _mediator.Send(new DeleteProductCommand { Id = id });
                return NoContent();
            }

        #endregion

    }
}
