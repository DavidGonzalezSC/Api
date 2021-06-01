using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class ServicioSucursales : IServiciosSucursales
    {
        private readonly SucursalesDbContext _mSucursales;

        public ServicioSucursales(SucursalesDbContext mSucursales)
        {
            _mSucursales = mSucursales ?? throw new ArgumentNullException(nameof(mSucursales));
        }

        public async Task<IEnumerable<Sucursal>> ListadoDeSucursales()
        {
            var param1 = "SC2017";
            var param2 = 1;
            var listado = await _mSucursales.DbSetSucursales.FromSqlRaw($"EXECUTE dbo.SP_ConexionListarSucursalMultinivel {param1},{param2}").ToListAsync();
            return listado;
        }

        public async Task<Sucursal> Sucursal(string codigo)
        {
            var listado = await ListadoDeSucursales();
            var sucu = listado.Where(s => s.Codigo == codigo).FirstOrDefault();
            return sucu;
        }
       
    }
}
