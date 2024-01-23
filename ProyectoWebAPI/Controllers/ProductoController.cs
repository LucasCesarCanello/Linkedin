using Microsoft.AspNetCore.Mvc;
using ProyectoDTO;
using ProyectoService;

namespace ProyectoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : Controller
    {
        private LogicaProducto logica = new LogicaProducto();

        [HttpGet("")]
        public IActionResult GetProductos()
        {
            if (logica.ObtenerProductos().Count == 0)
            {
                return NoContent();
            }
                return Ok(logica.ObtenerProductos());
            
            
        }

        [HttpPost("")]
        public IActionResult PostProducto([FromBody] ProductoDTO newProducto)
        {
            ResultadoValidacion res = logica.CrearProducto(newProducto);
            if (res.Success)
            {
                return Ok(newProducto);
            }

            return BadRequest("Se cargaron mal los datos");
            
        }

        [HttpPut("{id}")]

        public IActionResult PutProducto(int id, [FromBody] int stock)
        {
            ProductoDTO? productoActualizado = logica.ActualizarStockProducto(id, stock);
            if (productoActualizado != null)
            {
                return Ok(productoActualizado);
            }

            return BadRequest("No se ha encontrado el producto o el stock es incorrecto");
            
        }
    }
}
