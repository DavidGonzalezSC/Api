using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IMiscelaneos
    {
        Task<bool> guardarContactoSinPresupuestoNuevo(string sucursal, TratativaSinPResupuesto datos);
        Task<bool> editarContactoSinPresupuestoNuevo(string sucursal, TratativaSinPResupuesto datos);
        Task<List<TratativaSinPResupuesto>> listadoSeguimientosSinPresupuesto(string sucursal, int dias);
    }
}
