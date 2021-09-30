using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Helpers.Validaciones
{
    public interface IValidaciones
    {
        Task<List<ValidarInactivos>> ListaDeInactivos();
        Task<List<ValidarExcepcionComercial>> ExcepcionesComerciales();
        Task<List<ValidarArticuloProveedor>> ArticulosPorProveedor();
        Task<List<Transportes>> Transportes();
        Task<List<SucursalesValidarDepoTransporte>> DepoyTrasnporte();
        Task<List<ValidacionesSPConsulta>> traerListaSpConsulta(string sucursal);
        Task<List<ValidacionesSPBloqueantes>> traerListaSpBloqueantes(string sucursal);
        Task<List<ValidacionesDepoDeFabricantes>> traerFabricantes();

        //--Metodo Universal..
        Task<ConjuntoDeValidacion> ConjuntoDereglas(string sucursal);

    }
}
