
using ProyectoData;
using ProyectoDTO;
using ProyectoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoTest
{
    internal class ProductoTest
    {
        private LogicaProducto logica = new LogicaProducto();
        [SetUp]
        public void Setup()
        {
            Json.LimpiarProductosJson();
            CargaProductos();
        }
        #region Extra
        private void CargaProductos()
        {
            logica.CrearProducto(new ProductoDTO
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
            logica.CrearProducto(new ProductoDTO
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
            logica.CrearProducto(new ProductoDTO
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
        #endregion
        #region Test POST
        [Test]
        public void CrearProducto_Correcto()
        {
            ResultadoValidacion res = logica.CrearProducto(new ProductoDTO
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
            ResultadoValidacion res2 = logica.CrearProducto(new ProductoDTO
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
            ResultadoValidacion res3 = logica.CrearProducto(new ProductoDTO
            {
                NombreProducto = "ProductoTest3",
                Marca = "MarcaTest3",
                AltoCaja = 3,
                AnchoCaja = 3,
                ProfundidadCaja =3,
                PrecioUnitario = 3,
                StockMinimo = 3,
                CantidadStock = 30
            });
            Assert.IsTrue(res.Success);
            Assert.IsTrue(res2.Success);
            Assert.IsTrue(res3.Success);
            Assert.That(logica.ObtenerProductos().Count, Is.EqualTo(3));
        }

        [Test]
        public void CrearProducto_Incorrecto_DatosMal()
        {
            ResultadoValidacion res = logica.CrearProducto(new ProductoDTO
            {
                NombreProducto = "",
                Marca = "",
                AltoCaja = -1,
                AnchoCaja = -1,
                ProfundidadCaja = -1,
                PrecioUnitario = -1,
                StockMinimo = -1,
                CantidadStock = -1
            });
            Assert.IsFalse(res.Success);
            Assert.That(logica.ObtenerProductos().Count, Is.EqualTo(3));
        }
        #endregion
        #region Test PUT
        [Test]
        public void ActualizarStock_Correcto()
        {
            logica.ActualizarStockProducto(1, 10);
            Assert.That(logica.ObtenerProductos()[0].CantidadStock, Is.EqualTo(35));
        }

        [Test]
        public void ActualizarStock_Incorrecto_CantidadCompradaNegativa()
        {
            logica.ActualizarStockProducto(1, -10);
            Assert.That((logica.ObtenerProductos()[0]).CantidadStock, Is.EqualTo(25));
        }
        #endregion
    }
}
