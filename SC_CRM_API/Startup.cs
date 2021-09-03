using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SC_CRM_API.Contextos;
using SC_CRM_API.Interfaces;
using SC_CRM_API.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        
        public void ConfigureServices(IServiceCollection services)
        {
            //agregar cors
            services.AddCors();
            services.AddDbContext<SucursalesDbContext>(opciones => opciones.UseSqlServer(Configuration.GetConnectionString("Productivo")));
            services.AddDbContext<MensajeriaDbContext>();

            services.AddScoped<IServiciosSucursales, ServicioSucursales>();
            services.AddScoped<IEscrituraCRM, RepoEscrituraCRM>();
            services.AddScoped<IConsultasCRM, RepoConsultasCRM>();
            services.AddScoped<IMensajeria, RepoMensajeria>();
            services.AddControllers();
        }

        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           

            app.UseRouting();

            //OJALDRE ACA ------------ DANGER!!! ---------------- BUN
            app.UseCors(regla => regla.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
            //OJALDRE ACA ------------ DANGER!!! ---------------- BUN

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
