using Geolocation;
using ProyectoData;
using ProyectoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoService
{
    public class LogicaViaje
    {
        public ResultadoValidacion AsignarCamionetas(ViajeDTO newViaje)
        {
			ResultadoValidacion res = newViaje.validarFechas();
			bool coincidencia = CoincidenciaFechas(newViaje.FechaEntregaDesde, newViaje.FechaEntregaHasta);
			if (coincidencia)
			{
				res.Errores.Add("Rango coincidente de fechas entre viajes ya programados");
                res.Success = false;
			}
            if (!res.Success)
            {
                return res;
            }
			List<Camioneta> listaCamionetas = Json.LeerCamionetasJson();
            List<Viaje> listaViajesNuevos = new List<Viaje>();
            foreach (Camioneta camioneta in listaCamionetas)
            {
                Viaje viajeNuevo = new Viaje()
                {
                    FechaCreacion = DateTime.Now,
                    FechaEntregaDesde = newViaje.FechaEntregaDesde,
                    FechaEntregaHasta = newViaje.FechaEntregaHasta,
                    CamionetaAsignada = camioneta.Patente,
                };
                listaViajesNuevos.Add(viajeNuevo);
			}

            List<Compra> listaComprasOpen = Json.LeerComprasJson().Where(x=>
            x.EstadoCompra == EstadosCompras.OPEN &&
            x.FechaEntrega >= newViaje.FechaEntregaDesde &&
            x.FechaEntrega <= newViaje.FechaEntregaHasta
            ).ToList();

            foreach (Compra compra in listaComprasOpen)
            {
                Viaje? viajeCorrespondiente = BuscarViajeCorrespondiente(compra, listaViajesNuevos);
                if (viajeCorrespondiente != null)
                {
                    Viaje viajeAsignado = listaViajesNuevos.First(x => x.CamionetaAsignada == viajeCorrespondiente.CamionetaAsignada);
                    viajeAsignado.ComprasLlevadas.Add(compra.CodCompra);
                    viajeAsignado.PorcentajeCargado = viajeCorrespondiente.PorcentajeCargado;
                } 
                else
                {
                    compra.FechaEntrega.AddDays(14);
                    Json.GuardarComprasJson(compra);
                }
            }

            foreach (Viaje viaje in listaViajesNuevos)
            {
                Json.GuardarViajesJson(viaje);
            }

            return res;
		}

        private bool CoincidenciaFechas(DateTime fechaDesde,DateTime fechaHasta)
        {
            List<Viaje> listaViajes = Json.LeerViajesJson();
            return listaViajes.Any(x => fechaDesde <= x.FechaEntregaHasta || x.FechaEntregaDesde <= fechaHasta);
        }


        private Viaje? BuscarViajeCorrespondiente(Compra compraOpen, List<Viaje> listaViajesNuevos)
        {
            List<Camioneta> listaCamionetas = Json.LeerCamionetasJson();
            float volumenTotalCompra = compraOpen.CalcularVolumenTotal();
            Viaje? viajeCorrespondiente = null;
            double distanciaCompra = LogicaMetodos.DistanciaGeografica(compraOpen.LatitudDestino, compraOpen.LongitudDestino);

            foreach (Viaje v in listaViajesNuevos)
            {
                Camioneta camionetaViaje = listaCamionetas.First(x => x.Patente == v.CamionetaAsignada);
                float porcentajeCarga = volumenTotalCompra * 100 / camionetaViaje.VolumenCarga;
                if (compraOpen.EstadoCompra == EstadosCompras.OPEN
                    && (100 - v.PorcentajeCargado) >= porcentajeCarga
                    && distanciaCompra <= camionetaViaje.DistanciaMax)
                {
                    compraOpen.EstadoCompra = EstadosCompras.READY_TO_DISPATCH;
                    Json.GuardarComprasJson(compraOpen);
                    viajeCorrespondiente = v;
                    viajeCorrespondiente.PorcentajeCargado += v.PorcentajeCargado;
                }
            }
            return viajeCorrespondiente;
        }

        public List<ViajeDTO> ObtenerListaViajesDTO()
        {
            List<Viaje> listaViajesData = Json.LeerViajesJson();
            List<ViajeDTO> listaViajesDTO = new List<ViajeDTO>();
            foreach (Viaje viaje in listaViajesData)
            {
                ViajeDTO viajeDTO = new ViajeDTO()
                {
                    FechaEntregaDesde = viaje.FechaEntregaDesde,
                    FechaEntregaHasta = viaje.FechaEntregaHasta
                };

                listaViajesDTO.Add(viajeDTO);
            }
            return listaViajesDTO;
        }

        public List<Viaje> ObtenerListaViajesData()
        {
            List<Viaje> listaViajesData = Json.LeerViajesJson();
            return listaViajesData;
        }
    }
}
