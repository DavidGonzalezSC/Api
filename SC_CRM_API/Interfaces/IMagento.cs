using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IMagento
    {
        //-- Lectura de estatus
        Task<List<EstatusMagento>> OrdenesSinProcesar();
        Task<List<EstatusMagento>> OrdenesDeUltimas24Horas();
        Task<List<EstatusMagento>> OrdenesCanceladas();
        Task<List<EstatusMagento>> OrdenesProcessing();


    }
}
