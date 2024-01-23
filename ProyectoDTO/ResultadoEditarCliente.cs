using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDTO
{
	public class ResultadoEditarCliente
	{
        public bool Actualizado { get; set; }
        public bool Encontrado { get; set; }
        public List<string> Actualizaciones { get; set; } = new List<string>();
    }
}
