using Geolocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService
{
	public static class LogicaMetodos
	{
		public static double DistanciaGeografica(double latCliente, double longCliente)
		{
			Coordinate origen = new Coordinate(-31.25033, -61.4867);
			Coordinate destino = new Coordinate(latCliente, longCliente);
			double distancia = GeoCalculator.GetDistance(origen, destino, 1, DistanceUnit.Kilometers);
			return distancia;
		}
	}
}
