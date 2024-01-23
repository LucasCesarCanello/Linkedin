using ProyectoDTO;
using ProyectoData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMaps.LocationServices;
using System.Net;

namespace ProyectoService
{
	public class LogicaCliente
	{
		#region POST
		public ResultadoValidacion CargaCliente(ClienteDTO cliente)
		{
			ResultadoValidacion result = cliente.Validaciones();
			if (result.Success)
			{
				double[] latitudLongitud = CalculoLatitudLongitud(cliente.Direccion, cliente.Ciudad);
				double distancia = LogicaMetodos.DistanciaGeografica(latitudLongitud[0], latitudLongitud[1]);
				if (distancia > 750)
				{
					result.Success = false;
					result.Errores.Add("La distancia del cliente excede la distancia maxima de envio");
				}
				else
				{
					Cliente clienteData = new Cliente()
					{
						DNI = cliente.DNI,
						Nombre = cliente.Nombre,
						Apellido = cliente.Apellido,
						Email = cliente.Email,
						Telefono = cliente.Telefono,
						Latitud = latitudLongitud[0],
						Longitud = latitudLongitud[1],
						FechaNacimiento = cliente.FechaNacimiento,
						FechaCreacion = DateTime.Now,
					};
					Json.GuardarClientesJson(clienteData);
				}
			}
			return result;
		}
		#endregion
		#region GET
		public List<ClienteDTO> ObtenerClientes()
		{
			List<Cliente> listaClientesData = Json.LeerClientesJson();
			List<ClienteDTO> listaClientesDto= new List<ClienteDTO>();
			foreach (Cliente cliente in listaClientesData)
			{
				ClienteDTO clienteDTO = new ClienteDTO()
				{
					DNI = cliente.DNI,
					Nombre= cliente.Nombre,
					Apellido= cliente.Apellido,
					Email = cliente.Email,
					Telefono = cliente.Telefono,
					FechaNacimiento= cliente.FechaNacimiento,
				};
				listaClientesDto.Add(clienteDTO);
			}
			return listaClientesDto;
		}
		#endregion
		#region PUT
		public ResultadoEditarCliente EditarCliente(ClienteDTO cliente)
		{
			ResultadoEditarCliente result = new ResultadoEditarCliente();
			List<Cliente> listaClientes = Json.LeerClientesJson();
			Cliente? clienteActualizado = listaClientes.FirstOrDefault(x=> x.DNI == cliente.DNI);
			if (clienteActualizado != null)
			{
				result.Encontrado = true;
				if (!string.IsNullOrEmpty(cliente.Nombre))
				{
					clienteActualizado.Nombre = cliente.Nombre;
					result.Actualizaciones.Add("Nombre");
				}
				if (!string.IsNullOrEmpty(cliente.Apellido))
				{
					clienteActualizado.Nombre = cliente.Apellido;
					result.Actualizaciones.Add("Apellido");
				}
				if (cliente.Telefono > 0)
				{
					clienteActualizado.Telefono = Convert.ToInt32(cliente.Telefono);
					result.Actualizaciones.Add("Telefono");
				}
				if (!string.IsNullOrEmpty(cliente.Email))
				{
					clienteActualizado.Email = cliente.Email;
					result.Actualizaciones.Add("Email");
				}
				if (!string.IsNullOrEmpty(cliente.Ciudad) && !string.IsNullOrEmpty(cliente.Direccion))
				{
					double[] latitudLongitud = CalculoLatitudLongitud(cliente.Direccion, cliente.Ciudad);
					clienteActualizado.Latitud = latitudLongitud[0];
					clienteActualizado.Longitud = latitudLongitud[1];
					result.Actualizaciones.Add("Ubicacion");
				}
				if(cliente.FechaNacimiento != DateTime.MinValue)
				{
					clienteActualizado.FechaNacimiento = cliente.FechaNacimiento;
					result.Actualizaciones.Add("Fecha de nacimineto");
				}
				if(result.Actualizaciones.Count() > 0)
				{
					result.Actualizado = true;
					clienteActualizado.FechaActualizacion = DateTime.Now;
					Json.GuardarClientesJson(clienteActualizado);
				}
			}
			return result;
		}
		#endregion
		#region DELETE
		public ResultadoValidacion EliminarCliente(int dni)
		{
			ResultadoValidacion result = new ResultadoValidacion();
			
			List<Cliente> listaClientes = Json.LeerClientesJson();
			Cliente? clienteEliminado = listaClientes.FirstOrDefault(x=> x.DNI == dni);
			if (clienteEliminado != null)
			{
				result.Success = true;
				clienteEliminado.FechaEliminacion = DateTime.Now;
				Json.GuardarClientesJson(clienteEliminado);
			}
			return result;
		}
		#endregion
		#region Extras
		private double[] CalculoLatitudLongitud(string direccion, string ciudad)
		{
			string address = $"{direccion}, {ciudad}, Argentina";
			var locationService = new GoogleLocationService("AIzaSyBxQ5faAyn9K9_GRUvI8syHkU8iDoq3SjM");
			var point = locationService.GetLatLongFromAddress(address);
			double[] latLong = new double[2];
			latLong[0] = point.Latitude;
			latLong[1] = point.Longitude;
			return latLong;
		}
		#endregion

	}
}
