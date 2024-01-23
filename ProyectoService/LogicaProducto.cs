using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoData;
using ProyectoDTO;

namespace ProyectoService
{
    public class LogicaProducto
    {
        #region POST
        public ResultadoValidacion CrearProducto(ProductoDTO producto)
        {
            ResultadoValidacion res = producto.ValidarDatosProducto(producto);
            if (res.Success)
            {
                
                Producto productoNuevo = new Producto
                {
                    AltoCaja = producto.AltoCaja,
                    AnchoCaja = producto.AnchoCaja,
                    CantidadStock = producto.CantidadStock,
                    FechaActualizacion = DateTime.MinValue,
                    FechaCreacion = DateTime.Now,
                    Marca = producto.Marca,
                    NombreProducto = producto.NombreProducto,
                    PrecioUnitario = producto.PrecioUnitario,
                    ProfundidadCaja = producto.ProfundidadCaja,
                    StockMinimo = producto.StockMinimo
                };

                Json.GuardarProductosJson(productoNuevo);
            }

            return res;
        }
        #endregion
        #region PUT
        public ProductoDTO? ActualizarStockProducto(int id, int cantidadStock)
        {
            Producto? prodActualizar = Json.LeerProductosJson().FirstOrDefault(x => x.CodProducto == id);

            if (prodActualizar != null && cantidadStock >= 0)
            {                
                
                prodActualizar.CantidadStock += cantidadStock;
                prodActualizar.FechaActualizacion = DateTime.Now;
                Json.GuardarProductosJson(prodActualizar);
                                
                ProductoDTO prodActResponse = new ProductoDTO()
                {
                    NombreProducto = prodActualizar.NombreProducto,
                    Marca = prodActualizar.Marca,
                    AltoCaja = prodActualizar.AltoCaja,
                    AnchoCaja = prodActualizar.AnchoCaja,
                    ProfundidadCaja = prodActualizar.ProfundidadCaja,
                    PrecioUnitario = prodActualizar.PrecioUnitario,
                    StockMinimo = prodActualizar.StockMinimo,
                    CantidadStock = prodActualizar.CantidadStock
                    

                };
                return prodActResponse;

            }
            return null;
            
            
        }
        #endregion
        public List<ProductoDTO> ObtenerProductos()
        {
            List<Producto> listaProductosData = Json.LeerProductosJson();
            List<ProductoDTO> listaProductosDTO= new List<ProductoDTO>();
            foreach (Producto prod in listaProductosData)
            {
                ProductoDTO prodDto = new ProductoDTO()
                {
                    NombreProducto = prod.NombreProducto,   
                    Marca = prod.Marca,
                    AltoCaja = prod.AltoCaja,
                    AnchoCaja = prod.AnchoCaja,
                    ProfundidadCaja = prod.ProfundidadCaja,
                    PrecioUnitario = prod.PrecioUnitario,
                    StockMinimo = prod.StockMinimo,
                    CantidadStock = prod.CantidadStock
                };
                listaProductosDTO.Add(prodDto);
            }
            return listaProductosDTO;
        }
    }

}
