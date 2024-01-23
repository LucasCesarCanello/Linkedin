using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDTO
{
    public class CompraDTO
    {
        public int CodProductoVendido { get; set; }

        public int DNIComprador { get; set; }

        public int CantidadComprada { get; set; }

        public DateTime FechaEntrega { get; set; }

        public ResultadoValidacion ValidarDatosCompra()
        {
            ResultadoValidacion resValidacion = new ResultadoValidacion();
            if (CodProductoVendido <= 0)
            {
                resValidacion.Errores.Add("Código producto vendido no válido");
            }
            if (DNIComprador <= 0)
            {
                resValidacion.Errores.Add("DNI Comprador no válido");
            }
            if (CantidadComprada <= 0)
            {
                resValidacion.Errores.Add("Cantidad comprada no válida");
            }
            if (FechaEntrega <= DateTime.Now)
            {
                resValidacion.Errores.Add("Fecha de entrega no válida");
            }

            if (resValidacion.Errores.Count == 0)
            {
                resValidacion.Success = true;
            }

            return resValidacion;
        }
    }
}
