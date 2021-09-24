using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IPresupuestos
    {
        //-- Listados dwe Presupuestos
        Task<IEnumerable<TransaccionGuardada>> ListadoDePresupuestosAsync(string sucursal, int vendedor, int dias, string estados);
        Task<ClienteDeConsulta> ObtenerClienteParaSeguimiento(string sucursal, int idCliente);
        Task<bool> GuardarParametrosTratativa(string sucursal, SeguimientosPresupuestoTratativa tratativa);
    }
}
