using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SC_CRM_API.Helpers.Validaciones
{
    public class ReglasDeValidacion : IValidaciones
    {
        

        public ReglasDeValidacion()
        {
            //-- CAMBIAR EN PRODUCTIVO
            //cliente.BaseAddress = new Uri("http://192.168.0.18:6370/api/validar/");
            //cliente.DefaultRequestHeaders.Accept.Clear();
            //cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<ConjuntoDeValidacion> ConjuntoDereglas(string sucursal)
        {

            ConjuntoDeValidacion nuevo = new ConjuntoDeValidacion();
            using (var cliente = new HttpClient())
            {
                cliente.BaseAddress = new Uri("http://10.0.0.7:6370/api/validar/");
                cliente.DefaultRequestHeaders.Accept.Clear();
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                nuevo.ListaDeDepoTransporte = await DepoyTrasnporte(cliente);
                nuevo.ListaDeArticulosPorProveedor = await ArticulosPorProveedor(cliente);
                nuevo.ListaDeExcepcionesComerciales = await ExcepcionesComerciales(cliente);
                nuevo.ListaDeInactivos = await ListaDeInactivos(cliente);
                nuevo.ListaDeTransportes = await Transportes(cliente);
                nuevo.ListaSpConsulta = await traerListaSpConsulta(cliente, sucursal);
                nuevo.ListaSpBloqueantes = await traerListaSpBloqueantes(cliente, sucursal);

            }
            return nuevo;
        }

        public async Task<List<ValidarArticuloProveedor>> ArticulosPorProveedor(HttpClient cliente)
        {
            //ObtenerProveedores
            var dataProcesada = new List<ValidarArticuloProveedor>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"ObtenerProveedores");
                dataProcesada = JsonConvert.DeserializeObject<List<ValidarArticuloProveedor>>(respuesta);
            }
            catch (HttpRequestException)
            {
                ValidarArticuloProveedor mal = new ValidarArticuloProveedor();
                mal.CodigoArticulo = "404";
                mal.CodigoProveedor = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }


        public async Task<List<SucursalesValidarDepoTransporte>> DepoyTrasnporte(HttpClient cliente)
        {
            var dataProcesada = new List<SucursalesValidarDepoTransporte>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"SucursalesListadoDepoTransporte");
                dataProcesada = JsonConvert.DeserializeObject<List<SucursalesValidarDepoTransporte>>(respuesta);
            }
            catch (HttpRequestException)
            {
                SucursalesValidarDepoTransporte mal = new SucursalesValidarDepoTransporte();
                mal.transporte = "404";
                mal.deposito = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }

        public async Task<List<ValidarExcepcionComercial>> ExcepcionesComerciales(HttpClient cliente)
        {
            var dataProcesada = new List<ValidarExcepcionComercial>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"Excepciones");
                dataProcesada = JsonConvert.DeserializeObject<List<ValidarExcepcionComercial>>(respuesta);
            }
            catch (HttpRequestException)
            {
                ValidarExcepcionComercial mal = new ValidarExcepcionComercial();
                mal.Clasificacion = "404";
                mal.Descripcion = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }

        public async Task<List<ValidarInactivos>> ListaDeInactivos(HttpClient cliente)
        {
            
            var dataProcesada = new List<ValidarInactivos>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"Inactivos");
                dataProcesada = JsonConvert.DeserializeObject<List<ValidarInactivos>>(respuesta);
            }
            catch (HttpRequestException)
            {
                ValidarInactivos mal = new ValidarInactivos();
                mal.CodArticu = "404";
                mal.Deposito = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }

        public async Task<List<Transportes>> Transportes(HttpClient cliente)
        {
            var dataProcesada = new List<Transportes>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"Transportes");
                dataProcesada = JsonConvert.DeserializeObject<List<Transportes>>(respuesta);
            }
            catch (HttpRequestException)
            {
                Transportes mal = new Transportes();
                mal.CodigoTransporte = "404";
                mal.Nombre = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }

        public async Task<List<ValidacionesSPConsulta>> traerListaSpConsulta(HttpClient cliente, string sucursal)
        {
            var dataProcesada = new List<ValidacionesSPConsulta>();

            try
            {
                sucursal = sucursal.ToLower();
                var respuesta = await cliente.GetStringAsync($"SpConsulta/{sucursal}");
                dataProcesada = JsonConvert.DeserializeObject<List<ValidacionesSPConsulta>>(respuesta);
            }
            catch (HttpRequestException es)
            {
                Console.WriteLine(es.Message);
                ValidacionesSPConsulta mal = new ValidacionesSPConsulta();
                mal.Codigo = "404";
                mal.Nombre = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }

        public async Task<List<ValidacionesSPBloqueantes>> traerListaSpBloqueantes(HttpClient cliente, string sucursal)
        {
            var dataProcesada = new List<ValidacionesSPBloqueantes>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"SpBloqueante/{sucursal}");
                dataProcesada = JsonConvert.DeserializeObject<List<ValidacionesSPBloqueantes>>(respuesta);
            }
            catch (HttpRequestException)
            {
                ValidacionesSPBloqueantes mal = new ValidacionesSPBloqueantes();
                mal.Codigo = "404";
                mal.Nombre = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }

        public async Task<List<ValidacionesDepoDeFabricantes>> traerFabricantes(HttpClient cliente)
        {
            var dataProcesada = new List<ValidacionesDepoDeFabricantes>();

            try
            {
                var respuesta = await cliente.GetStringAsync($"Fabricantes");
                dataProcesada = JsonConvert.DeserializeObject<List<ValidacionesDepoDeFabricantes>>(respuesta);
            }
            catch (HttpRequestException)
            {
                ValidacionesDepoDeFabricantes mal = new ValidacionesDepoDeFabricantes();
                mal.Deposito = "404";
                mal.Fabrica = "404";
                dataProcesada.Add(mal);

            }

            return dataProcesada;
        }
    }
}
