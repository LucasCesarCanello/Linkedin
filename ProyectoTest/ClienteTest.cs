using ProyectoService;
using ProyectoDTO;
using ProyectoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace ProyectoTest
{
    internal class ClienteTest
    {
        private LogicaCliente logica = new LogicaCliente();
        [SetUp]
        public void Setup()
        {
			Json.LimpiarClientesJson();
			CargarClientes();
        }

        #region Extra
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

        private List<Cliente> ObtenerClientesData()
        {
            List<Cliente> clientes = Json.LeerClientesJson();
            return clientes;
        }
        #endregion
        #region Test POST
        [Test]
        public void CargarCliente_Correcto()
        {
			ClienteDTO cliente = new ClienteDTO() { DNI= 43955732, Nombre= "Mateo", Apellido="Lerda", Email="MateoLerda@gmail.com", Telefono=206289, Ciudad="Rafaela", Direccion="Cecilia Griersson 116", FechaNacimiento= new DateTime(2002,05,03)};
		    ResultadoValidacion result = logica.CargaCliente(cliente);
            Assert.IsTrue(result.Success);
            Assert.That(logica.ObtenerClientes()[1].DNI, Is.EqualTo(43955732));
        }
		[Test]
		public void CargarCliente_ErrorDatos()
		{
			ClienteDTO cliente = new ClienteDTO() { Nombre = "Mateo", Apellido = "Lerda", Ciudad = "Rafaela", Direccion = "Cecilia Griersson 116" };
			ResultadoValidacion result = logica.CargaCliente(cliente);
			Assert.IsFalse(result.Success);
			Assert.That(result.Errores.First(), Is.EqualTo("DNI no valido"));
            Assert.That(result.Errores.Count(), Is.EqualTo(4));
		}
		[Test]
		public void CargarCliente_ErrorDistanciaMayor()
		{
			ClienteDTO cliente = new ClienteDTO() { DNI = 43955732, Nombre = "Mateo", Apellido = "Lerda", Email = "MateoLerda@gmail.com", Telefono = 206289, Ciudad = "Ushuaia", Direccion = "Magallanes 889", FechaNacimiento = new DateTime(2002 - 05 - 03) };
			ResultadoValidacion result = logica.CargaCliente(cliente);
			Assert.IsFalse(result.Success);
			Assert.That(result.Errores.First(), Is.EqualTo("La distancia del cliente excede la distancia maxima de envio"));
			Assert.That(result.Errores.Count(), Is.EqualTo(1));
		}
		#endregion
		#region Test GET
		[Test]
		public void ObtenerClientes_Correcto()
		{
			List<ClienteDTO> clientes= logica.ObtenerClientes();
			Assert.That(clientes.Count(), Is.EqualTo(2));
		}
		#endregion
		#region Test PUT
		[Test]
		public void ModificarCliente_Correcto()
		{
			ClienteDTO cliente = new ClienteDTO() { DNI = 43955732, Ciudad = "Cordoba", Direccion = "Lugones 123" };
			ResultadoEditarCliente resultado = logica.EditarCliente(cliente);
			Assert.IsTrue(resultado.Encontrado);
			Assert.IsTrue(resultado.Actualizado);
			Assert.That(resultado.Actualizaciones.Count(), Is.EqualTo(1));
			Assert.That(ObtenerClientesData().First(x => x.DNI == cliente.DNI).Latitud, Is.EqualTo(-31.425238));
			Assert.That(ObtenerClientesData().First(x => x.DNI == cliente.DNI).FechaActualizacion.Date, Is.EqualTo(DateTime.Today));
		}
		[Test]
		public void ModificarCliente_IncorrectoNoEncontrado()
		{
			ClienteDTO cliente = new ClienteDTO() { DNI = 17757901, Ciudad = "Cordoba", Direccion = "Lugones 123" };
			ResultadoEditarCliente resultado = logica.EditarCliente(cliente);
			Assert.IsFalse(resultado.Encontrado);
		}
		[Test]
		public void ModificarCliente_IncorrectoNoEditado()
		{
			ClienteDTO cliente = new ClienteDTO() { DNI = 43955641 };
			ResultadoEditarCliente resultado = logica.EditarCliente(cliente);
			Assert.IsTrue(resultado.Encontrado);
			Assert.IsFalse(resultado.Actualizado);
		}
		#endregion
		#region Test DELETE
		[Test]
		public void EliminarCliente_Correcto()
		{
			int dni = 43955732;
			ResultadoValidacion result = logica.EliminarCliente(dni);
			Assert.IsTrue(result.Success);
			Assert.That(ObtenerClientesData().First(x => x.DNI == dni).FechaEliminacion.Date, Is.EqualTo(DateTime.Today));
		}
		[Test]
		public void EliminarCliente_IncorrectoNoEncontrado()
		{
			int dni = 45029420;
			ResultadoValidacion result = logica.EliminarCliente(dni);
			Assert.IsFalse(result.Success);
		}
		#endregion
	}
}
