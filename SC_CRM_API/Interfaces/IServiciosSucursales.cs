
using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IServiciosSucursales
    {
        Task <IEnumerable<Sucursal>> ListadoDeSucursales();
        Task<Sucursal> Sucursal(string codigo);
    }
}
