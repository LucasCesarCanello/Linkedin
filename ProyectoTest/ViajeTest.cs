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
    internal class ViajeTest
    {
        private LogicaViaje logica = new LogicaViaje();
        [SetUp]
        public void Setup()
        {
            Json.LimpiarViajesJson();
            Json.LimpiarProductosJson();
            Json.LimpiarClientesJson();
            Json.LimpiarComprasJson();
            CargarProductos();
            CargarClientes();
            CargarCompras();
        }
        #region Extra
        private void CargarViajes()
        {
            logica.AsignarCamionetas(new ViajeDTO() { FechaEntregaDesde = new DateTime(2024, 10, 01), FechaEntregaHasta = new DateTime(2024, 10, 06) });
            logica.AsignarCamionetas(new ViajeDTO() { FechaEntregaDesde = new DateTime(2024, 10, 07), FechaEntregaHasta = new DateTime(2024, 10, 10) });
            logica.AsignarCamionetas(new ViajeDTO() { FechaEntregaDesde = new DateTime(2024, 10, 11), FechaEntregaHasta = new DateTime(2024, 10, 15) });
        }
        private void CargarProductos()
        {
            LogicaProducto logicaProducto = new LogicaProducto();
            logicaProducto.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest",
                Marca = "MarcaTest",
                AltoCaja = 3,
                AnchoCaja = 3,
                ProfundidadCaja = 3,
                PrecioUnitario = 1,
                StockMinimo = 1,
                CantidadStock = 25
            });
            logicaProducto.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest2",
                Marca = "MarcaTest2",
                AltoCaja = 5,
                AnchoCaja = 5,
                ProfundidadCaja = 5,
                PrecioUnitario = 2,
                StockMinimo = 2,
                CantidadStock = 20
            });
            logicaProducto.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest3",
                Marca = "MarcaTest3",
                AltoCaja = 7,
                AnchoCaja = 7,
                ProfundidadCaja = 7,
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

            logicaCliente.CargaCliente(new ClienteDTO()
            {
                DNI = 45029420,
                Nombre = "Mijael",
                Apellido = "Segura",
                Email = "mijaelsegura@gmail.com",
                Telefono = 203014,
                Ciudad = "San Miguel de Tucumán",
                Direccion = "Lamadrid1315",
                FechaNacimiento = new DateTime(2003, 07, 03)
            });

            logicaCliente.CargaCliente(new ClienteDTO()
            {
                DNI = 45944100,
                Nombre = "Lucas",
                Apellido = "Canello",
                Email = "lucascanello@gmail.com",
                Telefono = 206590,
                Ciudad = "Resistencia",
                Direccion = "Cervantes 165",
                FechaNacimiento = new DateTime(2004, 04, 14)
            });
        }

        private void CargarCompras()
        {
            LogicaCompra logicaCompra = new LogicaCompra();
            logicaCompra.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 1,
                DNIComprador = 43955641,
                CantidadComprada = 5,
                FechaEntrega = new DateTime(2024, 10, 01)
            });

            logicaCompra.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 2,
                DNIComprador = 45029420,
                CantidadComprada = 5,
                FechaEntrega = new DateTime(2024, 10, 02)
            });

            logicaCompra.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 3,
                DNIComprador = 45944100,
                CantidadComprada = 3,
                FechaEntrega = new DateTime(2024, 10, 03)
            });

            logicaCompra.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 1,
                DNIComprador = 43955641,
                CantidadComprada = 5,
                FechaEntrega = new DateTime(2024, 10, 04)
            }); 

            logicaCompra.CrearCompra(new CompraDTO()
            {
                CodProductoVendido = 2,
                DNIComprador = 45944100,
                CantidadComprada = 1,
                FechaEntrega = new DateTime(2024,10,05)
            });
        }
        #endregion

        [Test]
        public void Test_AsignarCamionetas_Correcto()
        {
            ResultadoValidacion res1 = logica.AsignarCamionetas(new ViajeDTO() { FechaEntregaDesde = new DateTime(2024,10,01), FechaEntregaHasta = new DateTime(2024,10,06)});

            Assert.That(logica.ObtenerListaViajesDTO().Count, Is.EqualTo(3));
            Assert.That(logica.ObtenerListaViajesData()[0].ComprasLlevadas[0], Is.EqualTo(1));
            Assert.That(logica.ObtenerListaViajesData()[0].ComprasLlevadas[1], Is.EqualTo(4));
            Assert.That(logica.ObtenerListaViajesData()[1].ComprasLlevadas[0], Is.EqualTo(3));
            Assert.That(logica.ObtenerListaViajesData()[1].ComprasLlevadas[1], Is.EqualTo(5));
            Assert.That(logica.ObtenerListaViajesData()[2].ComprasLlevadas[0], Is.EqualTo(2));
            Assert.IsTrue(res1.Success);

        }


        [Test]
        public void Test_AsignarCamionetas_Incorrecto_ErrorFechas()
        {
            CargarViajes();
            ResultadoValidacion res = logica.AsignarCamionetas(new ViajeDTO() { FechaEntregaDesde = DateTime.Now.AddDays(-2),FechaEntregaHasta = DateTime.Now.AddDays(10)});
            Assert.IsFalse(res.Success);
            Assert.That(res.Errores[0], Is.EqualTo("Fecha desde menor al día de hoy"));
            Assert.That(res.Errores[1], Is.EqualTo("Intervalo entre fecha desde y fecha hasta mayor a 7 días"));
            Assert.That(logica.ObtenerListaViajesDTO().Count, Is.EqualTo(3));
        }

        [Test]
        public void Test_AsignarCamionetas_Incorrecto_CoincidenciaFechas()
        {
            CargarViajes();
            ResultadoValidacion res = logica.AsignarCamionetas(new ViajeDTO() { FechaEntregaDesde = new DateTime(2024, 10, 05), FechaEntregaHasta = new DateTime(2024, 10, 11)});
            Assert.IsFalse(res.Success);
            Assert.That(res.Errores[0], Is.EqualTo("Rango coincidente de fechas entre viajes ya programados"));
            Assert.That(logica.ObtenerListaViajesDTO().Count, Is.EqualTo(3));
        }
    }
}
