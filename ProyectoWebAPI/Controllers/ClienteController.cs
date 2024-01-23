using Microsoft.AspNetCore.Mvc;
using ProyectoDTO;
using ProyectoService;

namespace ProyectoWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
	public class ClienteController : Controller
    {
			private LogicaCliente logica = new LogicaCliente();

			[HttpPost("")]
			public IActionResult PostCliente([FromBody] ClienteDTO cliente)
			{
				ResultadoValidacion result = logica.CargaCliente(cliente);
				if (result.Success)
				{
					return Ok($"Se cargo correctamente el cliente DNI= {cliente.DNI}");
				}

				return BadRequest(result.Errores);
				
			}

			[HttpGet("")]
			public IActionResult GetClientes()
			{
				List<ClienteDTO> clientes = logica.ObtenerClientes();
				if (clientes.Count == 0)
				{
					return NoContent();
				}
				return Ok(clientes);
			}

			[HttpPut("")]
			public IActionResult PutCliente([FromBody] ClienteDTO cliente)
			{
				ResultadoEditarCliente result = logica.EditarCliente(cliente);

				if (!result.Encontrado)
				{
					return NotFound($"No existe cliente con DNI= {cliente.DNI}");
				}
				else if (!result.Actualizado)
				{
					return BadRequest("No se ingreso ningun campo actualizable");
				}

				return Ok(result.Actualizaciones);
				
			}

			[HttpDelete("{dni}")]
			public IActionResult DeleteCliente(int dni)
			{
				ResultadoValidacion result = logica.EliminarCliente(dni);
				if (!result.Success)
				{
					return NotFound($"No existe cliente con DNI= {dni}");
				}

				return Ok($"Se elimino el cliente DNI= {dni}");
				
			}
		
	}
}
