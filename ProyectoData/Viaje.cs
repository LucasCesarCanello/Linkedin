using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Viaje
    {
        public int CodViaje {  get; set; }

        public string CamionetaAsignada { get; set; }

        public DateTime FechaEntregaDesde { get; set; }

        public DateTime FechaEntregaHasta { get; set; }

        public double PorcentajeCargado { get; set; }

        public List<int> ComprasLlevadas { get; set; } = new List<int>();

        public DateTime FechaCreacion { get; set; }

    }
}
