using ProyectoData;
using ProyectoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService
{
    public class LogicaCompra
    {
        public ResultadoValidacion CrearCompra(CompraDTO newCompraDTO)
        {
            ResultadoValidacion validacionDatos = newCompraDTO.ValidarDatosCompra();
            Producto? prodVendido = Json.LeerProductosJson().FirstOrDefault(x => x.CodProducto == newCompraDTO.CodProductoVendido);
            Cliente? clienteComprador = Json.LeerClientesJson().FirstOrDefault(x => x.DNI == newCompraDTO.DNIComprador);
            if (prodVendido == null)
            {
                validacionDatos.Errores.Add("No existe producto con ese código");
                validacionDatos.Success = false;
            }
            else if (prodVendido.CantidadStock < newCompraDTO.CantidadComprada || (prodVendido.CantidadStock - newCompraDTO.CantidadComprada) < prodVendido.StockMinimo)
            {
                validacionDatos.Errores.Add("No hay suficiente stock");
                validacionDatos.Success = false;
            }

            if (clienteComprador == null)
            {
                validacionDatos.Errores.Add("Cliente con ese DNI no encontrado");
                validacionDatos.Success = false;
            }

            if (validacionDatos.Success)
            {
                Compra newCompraData = new Compra
                {
                    CodProductoVendido = newCompraDTO.CodProductoVendido,
                    DNIComprador = newCompraDTO.DNIComprador,
                    FechaCompra = DateTime.Now,
                    CantidadComprada = newCompraDTO.CantidadComprada,
                    FechaEntrega = newCompraDTO.FechaEntrega,
                    EstadoCompra = EstadosCompras.OPEN,
                };
                newCompraData.MontoTotal = newCompraData.CalcularMontoTotal(prodVendido);
                newCompraData.LatitudDestino = clienteComprador.Latitud;
                newCompraData.LongitudDestino = clienteComprador.Longitud;
                prodVendido.CantidadStock -= newCompraDTO.CantidadComprada;
                Json.GuardarProductosJson(prodVendido);
                Json.GuardarComprasJson(newCompraData);
            }

            return validacionDatos;
        }

        public List<CompraDTO> ObtenerListaCompras()
        {
            List<Compra> listaComprasData = Json.LeerComprasJson();
            List<CompraDTO> listaComprasDTO = new List<CompraDTO>();
            foreach (Compra compra in listaComprasData)
            {
                CompraDTO compraDTO = new CompraDTO()
                {
                    CodProductoVendido = compra.CodProductoVendido,
                    CantidadComprada = compra.CantidadComprada,
                    DNIComprador = compra.DNIComprador,
                    FechaEntrega = compra.FechaEntrega,
                };
                listaComprasDTO.Add(compraDTO);
            }
            return listaComprasDTO;
        }
    }
}
