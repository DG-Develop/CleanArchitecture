using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        [HttpGet]
        public IActionResult GetProducts()
        {
            // Aqu� ir�a la l�gica para obtener los productos, por ejemplo, llamando a un servicio o repositorio.
            // Por ahora, devolvemos un mensaje de ejemplo.
            return Ok(new { Message = "Lista de productos" });
        }


    }
}