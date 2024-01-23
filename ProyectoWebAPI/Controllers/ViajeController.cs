using Microsoft.AspNetCore.Mvc;
using ProyectoDTO;
using ProyectoService;

namespace ProyectoWebAPI.Controllers
{
    public class ViajeController : Controller
    {
        LogicaViaje logica = new LogicaViaje();
        [HttpPost("")]
       public IActionResult PostViajes([FromBody] ViajeDTO newViaje)
        {
            ResultadoValidacion res = logica.AsignarCamionetas(newViaje);
            if (res.Success)
            {
                return Ok("Viaje asignado con éxito");
            }
            else
            {
                return BadRequest("No se ha podido asignar el viaje debido a las fechas del mismo");
            }
        }
    }
}
