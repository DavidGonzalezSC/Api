using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SC_CRM_API.Helpers.Validaciones
{
    public interface IValidaciones
    {
        Task<List<ValidarInactivos>> ListaDeInactivos(HttpClient cliente);
        Task<List<ValidarExcepcionComercial>> ExcepcionesComerciales(HttpClient cliente);
        Task<List<ValidarArticuloProveedor>> ArticulosPorProveedor(HttpClient cliente);
        Task<List<Transportes>> Transportes(HttpClient cliente);
        Task<List<SucursalesValidarDepoTransporte>> DepoyTrasnporte(HttpClient cliente);
        Task<List<ValidacionesSPConsulta>> traerListaSpConsulta(HttpClient cliente, string sucursal);
        Task<List<ValidacionesSPBloqueantes>> traerListaSpBloqueantes(HttpClient cliente, string sucursal);
        Task<List<ValidacionesDepoDeFabricantes>> traerFabricantes(HttpClient cliente);

        //--Metodo Universal..
        Task<ConjuntoDeValidacion> ConjuntoDereglas(string sucursal);

    }
}
