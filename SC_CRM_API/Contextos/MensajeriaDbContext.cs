using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Contextos
{
    public class MensajeriaDbContext : DbContext
    {
        public DbSet<Email> MailsAEnviar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(
                    @"Server=192.168.0.5;Initial Catalog=ServicioMails;User Id=Sa;Password=SC2020$;",
                    options => options.EnableRetryOnFailure());
        }

    }
}
