using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IMagento
    {
        //-- Lectura de estatus v1 medio obsoletos
        Task<List<EstatusMagentoV1>> OrdenesSinProcesarV1();
        Task<List<EstatusMagentoV1>> OrdenesDeUltimas24HorasV1();
        Task<List<EstatusMagentoV1>> OrdenesCanceladasV1();
        Task<List<EstatusMagentoV1>> OrdenesProcessingV1();

        Task<int> EscribirOrdenV1(EstatusMagentoV1 magento);

        //-- Lectura de status con V2
        Task<List<EstatusMagentoV2>> OrdenesSinProcesarV2();

        //-- Ir a buscar datos de la transaccion
        Task<int> ObtenerDatosExternos(string numeroDeOrden);


    }
}
