using Microsoft.AspNetCore.Mvc;
using ProyectoDTO;
using ProyectoService;

namespace ProyectoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompraController : Controller
    {
        private LogicaCompra logica = new LogicaCompra();

        [HttpPost("")]
        public IActionResult CrearCompra([FromBody] CompraDTO compraRequest)
        {
            ResultadoValidacion res = logica.CrearCompra(compraRequest);
            if (!res.Success)
            {
                return BadRequest(res.Errores);
            }

            return Ok(compraRequest);
        }

        [HttpGet("")]
        public IActionResult ObtenerCompras()
        {
            List<CompraDTO> listaCompras = logica.ObtenerListaCompras();
            if (listaCompras.Count == 0)
            {
                return NoContent();
            }

            return Ok(listaCompras);
        }
    }
}
