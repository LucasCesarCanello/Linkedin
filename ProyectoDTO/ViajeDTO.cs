using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDTO
{
    public class ViajeDTO
    {
        public DateTime FechaEntregaDesde { get; set; }

        public DateTime FechaEntregaHasta { get; set; }

        public ResultadoValidacion validarFechas()
        {
            ResultadoValidacion res = new ResultadoValidacion();
            if (FechaEntregaDesde < DateTime.Now)
            {
                res.Errores.Add("Fecha desde menor al día de hoy");
            }

            if ((FechaEntregaHasta - FechaEntregaDesde).Days > 7)
            {
                res.Errores.Add("Intervalo entre fecha desde y fecha hasta mayor a 7 días");
            }

            if (FechaEntregaDesde > FechaEntregaHasta)
            {
                res.Errores.Add("Fecha desde mayor a fecha hasta");
            }

            if (res.Errores.Count == 0)
            {
                res.Success = true;
            }

            return res;
        }
    }
}
