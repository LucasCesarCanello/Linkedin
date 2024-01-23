using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProyectoData
{
    public class Json
    {
        #region GUARDAR EN JSON
        public static void GuardarProductosJson(Producto dataProd)
        {
            var listaProd = LeerProductosJson();

            if (listaProd.Any(x => x.NombreProducto == dataProd.NombreProducto && x.Marca == dataProd.Marca))
            {
                dataProd.CodProducto = listaProd.Where(x=>x.NombreProducto == dataProd.NombreProducto && x.Marca == dataProd.Marca).Select(x => x.CodProducto).First();
                listaProd.RemoveAll(x => x.NombreProducto == dataProd.NombreProducto && x.Marca == dataProd.Marca);
            } 
            else
            {
                dataProd.CodProducto = listaProd.Count + 1;
            }
            listaProd.Add(dataProd);
            listaProd= listaProd.OrderBy(x=> x.CodProducto).ToList();
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "productos.json");
            string json = JsonConvert.SerializeObject(listaProd, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static void GuardarClientesJson(Cliente dataCli)
        {
            var listaClientes = LeerClientesJson();
            
            if (listaClientes.Any(x=>x.DNI == dataCli.DNI))
            {
                listaClientes.RemoveAll(x => x.DNI == dataCli.DNI);
            }

            listaClientes.Add(dataCli);
            listaClientes = listaClientes.OrderBy(X=> X.DNI).ToList();
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "clientes.json");
            string json = JsonConvert.SerializeObject(listaClientes, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static void GuardarComprasJson(Compra dataCompra)
        {
            var listaCompras = LeerComprasJson();
            if (dataCompra.CodCompra != 0)
            {
                listaCompras.RemoveAll(x => x.CodCompra == dataCompra.CodCompra);
            } 
            else
            {
                dataCompra.CodCompra = listaCompras.Count + 1;
            }

            listaCompras.Add(dataCompra);

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "compras.json");
            string json = JsonConvert.SerializeObject(listaCompras, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static void GuardarViajesJson(Viaje dataViaje)
        {
            var listaViajes = LeerViajesJson();
            dataViaje.CodViaje = listaViajes.Count + 1;

            listaViajes.Add(dataViaje);

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "viajes.json");
            string json = JsonConvert.SerializeObject(listaViajes, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        #endregion
        #region LEER DESDE JSON
        public static List<Producto> LeerProductosJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "productos.json");
            
            if (File.Exists(rutaArchivo))
            {
                return JsonConvert.DeserializeObject<List<Producto>>(File.ReadAllText(rutaArchivo));
            } 
            else
            {
                return new List<Producto>();
            }
        }
        public static List<Cliente> LeerClientesJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "clientes.json");

            if (File.Exists(rutaArchivo))
            {
                return JsonConvert.DeserializeObject<List<Cliente>>(File.ReadAllText(rutaArchivo));
            }
            else
            {
                return new List<Cliente>();
            }
        }
        public static List<Compra> LeerComprasJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "compras.json");

            if (File.Exists(rutaArchivo))
            {
                return JsonConvert.DeserializeObject<List<Compra>>(File.ReadAllText(rutaArchivo));
            }
            else
            {
                return new List<Compra>();
            }
        }

        public static List<Viaje> LeerViajesJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "viajes.json");

            if (File.Exists(rutaArchivo))
            {
                return JsonConvert.DeserializeObject<List<Viaje>>(File.ReadAllText(rutaArchivo));
            }
            else
            {
                return new List<Viaje>();
            }
        }

        public static List<Camioneta> LeerCamionetasJson()
        {
            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "camionetas.json");

            if (File.Exists(rutaArchivo))
            {
                List<Camioneta> lista = JsonConvert.DeserializeObject<List<Camioneta>>(File.ReadAllText(rutaArchivo));
                lista = lista.OrderBy(x => x.DistanciaMax).ToList();
                return lista;
            }
            else
            {
                return new List<Camioneta>();
            }
        }
        #endregion

        #region LIMPIAR LISTAS (PARA TEST)
        public static void LimpiarProductosJson()
        {
            var listaProd = LeerProductosJson();
            listaProd.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "productos.json");
            string json = JsonConvert.SerializeObject(listaProd, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static void LimpiarClientesJson()
        {
            var listaClientes = LeerClientesJson();
            listaClientes.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "clientes.json");
            string json = JsonConvert.SerializeObject(listaClientes, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static void LimpiarComprasJson()
        {
            var listaCompras = LeerComprasJson();
            listaCompras.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "compras.json");
            string json = JsonConvert.SerializeObject(listaCompras, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }

        public static void LimpiarViajesJson()
        {
            var listaViajes = LeerViajesJson();
            listaViajes.Clear();

            string rutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "viajes.json");
            string json = JsonConvert.SerializeObject(listaViajes, Formatting.Indented);
            File.WriteAllText(rutaArchivo, json);
        }
        #endregion
    }
}
