﻿using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Contextos
{
    public class MagentoDbContext : DbContext
    {
        public DbSet<EstatusMagentoV1> DbMagentoStatusV1 { get; set; }
        public DbSet<EstatusMagentoV2> DbMagentoStatusV2 { get; set; }

        //Para actualizar estados
        public MagentoDbContext(DbContextOptions<MagentoDbContext> opciones)
            : base(opciones)
        {
        }
    }
}
