using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Compra
    {
        public int CodCompra {  get; set; }

        public int CodProductoVendido { get; set; }

        public int DNIComprador { get; set; }

        public DateTime FechaCompra { get; set; }

        public int CantidadComprada { get; set; }

        public DateTime FechaEntrega { get; set; }

        public EstadosCompras EstadoCompra { get; set; }

        public double MontoTotal { get; set; }

        public double LatitudDestino {  get; set; }

        public double LongitudDestino { get; set; }


        public double CalcularMontoTotal(Producto prodVendido)
        {
            double montoTotal = (CantidadComprada * prodVendido.PrecioUnitario) * 1.21;
            if (CantidadComprada > 4)
            {
                montoTotal -= montoTotal * 0.25;
            }
            return montoTotal;
        }
        public float CalcularVolumenTotal()
        {
            float volumen = Json.LeerProductosJson().First(x => x.CodProducto == CodProductoVendido).CalculoVolumen();
            return volumen * CantidadComprada;
        }

        
    }
}
