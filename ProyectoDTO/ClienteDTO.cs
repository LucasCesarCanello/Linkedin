using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoDTO
{
    public class ClienteDTO
    {
        public int DNI { get; set; }

        public string? Nombre { get; set; }

        public string? Apellido { get; set; }

        public string? Email { get; set; }

        public int Telefono { get; set; }

        public string? Direccion { get; set; }

        public string? Ciudad { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public ResultadoValidacion Validaciones()
        {
			ResultadoValidacion result = new ResultadoValidacion();
			if (DNI <= 0)
			{
				result.Errores.Add("DNI no valido");
			}
			if (string.IsNullOrEmpty(Nombre))
            {
				result.Errores.Add("Nombre no ingresado");
			}
			if (string.IsNullOrEmpty(Apellido))
			{
				result.Errores.Add("Apellido no ingresado");
			}
			if (string.IsNullOrEmpty(Email))
			{
				result.Errores.Add("Email no ingresado");
			}
			if (Telefono <= 0)
			{
				result.Errores.Add("Telefono no valido");
			}
			if (string.IsNullOrEmpty(Direccion))
			{
				result.Errores.Add("Direccion no ingresada");
			}
			if (string.IsNullOrEmpty(Ciudad))
			{
				result.Errores.Add("Ciudad no ingresada");
			}
			if(FechaNacimiento == DateTime.MinValue)
			{
				result.Errores.Add("Fecha de Nacimiento no ingresada");
			}

			if (result.Errores.Count == 0)
			{
				result.Success = true;
			}
			return result;
		}
    }
}
