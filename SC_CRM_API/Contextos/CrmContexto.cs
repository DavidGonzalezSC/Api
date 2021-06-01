using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SC_CRM_API.Contextos
{
    class CrmContexto : DbContext
    {

        public string Servidor { get; private set; }
        public string Usuario { get; private set; }
        public string Password { get; private set; }
        public string Catalogo { get; private set; }

        public CrmContexto(Sucursal sucursal)
        {
            this.Servidor = sucursal.Servidor;
            this.Usuario = sucursal.Usuario;
            this.Password = sucursal.Clave;
            this.Catalogo = sucursal.Base;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexion = $"Server={Servidor};Initial Catalog={Catalogo};User Id={Usuario};Password={Password}";
            optionsBuilder
                .UseSqlServer(
                    conexion,
                    options => options.CommandTimeout(480));
        }

        //el sp no devuelve key
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SqlRespuesta>().HasNoKey();
            
        }


        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<ClienteDeConsulta> ClientesDeConsulta { get; set; }
        public DbSet<Presupuesto> Presupuestos { get; set; }
        public DbSet<Detalle> Detalles { get; set; }
        public DbSet<DireccionDeEntrega> DireccionDeEntregas { get; set; }
        public DbSet<SqlRespuesta> RespuestaEscritura { get; set; }
    }
}
