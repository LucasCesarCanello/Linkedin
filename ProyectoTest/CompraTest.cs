using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoData;
using ProyectoDTO;
using ProyectoService;

namespace ProyectoTest
{
    internal class CompraTest
    {
        private LogicaCompra logica = new LogicaCompra();
        [SetUp]
        public void Setup()
        {
            Json.LimpiarProductosJson();
            Json.LimpiarClientesJson();
            Json.LimpiarComprasJson();
            CargarProductos();
            CargarClientes();
        }
        #region Extras
        private void CargarProductos()
        {
            LogicaProducto logicaProducto = new LogicaProducto();
            logicaProducto.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest",
                Marca = "MarcaTest",
                AltoCaja = 1,
                AnchoCaja = 1,
                ProfundidadCaja = 1,
                PrecioUnitario = 1,
                StockMinimo = 1,
                CantidadStock = 25
            });
            logicaProducto.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest2",
                Marca = "MarcaTest2",
                AltoCaja = 2,
                AnchoCaja = 2,
                ProfundidadCaja = 2,
                PrecioUnitario = 2,
                StockMinimo = 2,
                CantidadStock = 20
            });
            logicaProducto.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest3",
                Marca = "MarcaTest3",
                AltoCaja = 3,
                AnchoCaja = 3,
                ProfundidadCaja = 3,
                PrecioUnitario = 3,
                StockMinimo = 3,
                CantidadStock = 30
            });
        }
        private void CargarClientes()
        {
            LogicaCliente logicaCliente = new LogicaCliente();
            logicaCliente.CargaCliente(new ClienteDTO()
            {
                DNI = 43955641,
                Nombre = "Delfina",
                Apellido = "Perez",
                Email = "DelfinaPerez02@gmail.com",
                Telefono = 4566732,
                Ciudad = "Rafaela",
                Direccion = "Los Cedros 1971",
                FechaNacimiento = new DateTime(2002, 04, 02)
            });

            logicaCliente.CargaCliente(new ClienteDTO()
            {
                DNI = 43955732,
                Nombre = "Mateo",
                Apellido = "Lerda",
                Email = "MateoLerda@gmail.com",
                Telefono = 206289,
                Ciudad = "Rafaela",
                Direccion = "Cecilia Griersson 116",
                FechaNacimiento = new DateTime(2002, 05, 03)
            });
        }
        #endregion

        #region Test POST
        [Test]
        public void TestCrearCompra_Correcto()
        {
            ResultadoValidacion res = logica.CrearCompra(new CompraDTO() 
            { 
                CodProductoVendido = 1, 
                DNIComprador = 43955641, 
                CantidadComprada = 10, 
                FechaEntrega = new DateTime(2023,12,25)
            });
            Assert.IsTrue(res.Success);
            Assert.That(res.Errores.Count, Is.EqualTo(0));
            LogicaProducto stock = new LogicaProducto();
            Assert.That(stock.ObtenerProductos()[0].CantidadStock, Is.EqualTo(15));
        }

        [Test]
        public void TestCrearCompra_Incorrecto_FaltanDatos()
        {
            ResultadoValidacion res = logica.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 1,
            });
            Assert.IsFalse(res.Success);
            Assert.That(res.Errores[0], Is.EqualTo("DNI Comprador no válido")); 
            Assert.That(res.Errores[1], Is.EqualTo("Cantidad comprada no válida"));
            Assert.That(res.Errores[2], Is.EqualTo("Fecha de entrega no válida"));
            Assert.That(logica.ObtenerListaCompras().Count, Is.EqualTo(0));
        }

        [Test]
        public void TestCrearCompra_Incorrecto_ErrorCliente_ErrorStock()
        {
            ResultadoValidacion res = logica.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 1,
                CantidadComprada = 30,
                DNIComprador = 45029420,
                FechaEntrega = DateTime.Now.AddDays(7),
            });
            Assert.IsFalse(res.Success);
            Assert.That(res.Errores[0], Is.EqualTo("No hay suficiente stock"));
            Assert.That(res.Errores[1], Is.EqualTo("Cliente con ese DNI no encontrado"));
            Assert.That(logica.ObtenerListaCompras().Count, Is.EqualTo(0));
        }
        #endregion
    }
}
