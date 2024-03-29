﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Cliente
    {
        public int DNI {  get; set; }

        public string Nombre {  get; set; }

        public string Apellido {  get; set; }

        public string Email {  get; set; }

        public int Telefono { get; set; }

        public double Latitud {  get; set; }

        public double Longitud { get; set; }

        public DateTime FechaNacimiento { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

        public DateTime FechaEliminacion { get; set; }

	}
}
