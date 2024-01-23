using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Producto
    {
        public int CodProducto { get; set; }

        public string? NombreProducto { get; set; }

        public string? Marca { get; set; }

        public float AltoCaja { get; set; }

        public float AnchoCaja { get; set; }

        public float ProfundidadCaja { get; set; }

        public int PrecioUnitario { get; set; }

        public int StockMinimo { get; set; }

        public int CantidadStock { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

       public float CalculoVolumen()
       {
            float volumen = ProfundidadCaja * AltoCaja * AltoCaja;
            return volumen;
       }
    }
}
