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

        public MensajeriaDbContext(DbContextOptions<MensajeriaDbContext> opciones)
           : base(opciones)
        {
        }

    }
}
