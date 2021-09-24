using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IMensajeria
    {
        Task<string> confeccionarPdf(MailDto parametros);
        Task<bool> guardarParaEnvio(Email mensaje);
        IEnumerable<string> ListarPlantillas();
    }
}
