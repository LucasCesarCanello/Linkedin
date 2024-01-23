using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDTO
{
    public class ProductoDTO
    {
        public string? NombreProducto { get; set; }

        public string? Marca { get; set; }

        public float AltoCaja { get; set; }

        public float AnchoCaja { get; set; }

        public float ProfundidadCaja { get; set; }

        public int PrecioUnitario { get; set; }

        public int StockMinimo { get; set; }
    

        public int CantidadStock { get; set; }

        public ResultadoValidacion ValidarDatosProducto(ProductoDTO producto)
        {
            ResultadoValidacion res = new ResultadoValidacion();
            if (string.IsNullOrEmpty(NombreProducto))
            {
                res.Errores.Add("Falta nombre");
            }
            if (string.IsNullOrEmpty(Marca))
            {
                res.Errores.Add("Falta marca");

            }
            if (producto.AltoCaja<=0)
            {
                res.Errores.Add("Falta alto");
            }
            if (AnchoCaja<=0)
            {
                res.Errores.Add("Falta ancho");
            }
            if (ProfundidadCaja<=0)
            {
                res.Errores.Add("Falta profundidad");
            }
            if (PrecioUnitario<=0)
            {
                res.Errores.Add("Falta precio");
            }
            if (StockMinimo<=0)
            {
                res.Errores.Add("Falta stock minimo");
            }
            if (CantidadStock<=0)
            {
                res.Errores.Add("Falta cantidad stock");
            }

            if (res.Errores.Count == 0)
            {
                res.Success=true;
            }
         

            return res;
        }
    }
}
