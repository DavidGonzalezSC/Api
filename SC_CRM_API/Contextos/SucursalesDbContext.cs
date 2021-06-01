using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Contextos
{
    public class SucursalesDbContext : DbContext
    {
        public DbSet<Sucursal> DbSetSucursales { get; set; }

        //Para actualizar estados
        public SucursalesDbContext(DbContextOptions<SucursalesDbContext> opciones)
            : base(opciones)
        {
        }
        
    }
}
