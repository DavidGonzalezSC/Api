using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoPresupuestos : IPresupuestos
    {
        private readonly IServiciosSucursales _sucursales;

        public RepoPresupuestos(IServiciosSucursales sucursales)
        {
            _sucursales = sucursales;
        }

        public async Task<Sucursal> credencialesAsync(string sucursal)
        {
            Sucursal credenciales = await _sucursales.Sucursal(sucursal);

            if (credenciales != null) //forzar parametros de prueba
                return credenciales;
            else
                return null;

        }

        public async Task<bool> GuardarParametrosTratativa(string sucursal, SeguimientosPresupuestoTratativa tratativa)
        {
            int escribio = 0;

            Sucursal contexto = await credencialesAsync(sucursal);
            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                var presupuesto = _crmDbContext.PresupuestosParaConsulta.Where(i => i.IdPresupuesto == tratativa.Idpresupuesto).FirstOrDefault();
                presupuesto.Comentarios = tratativa.Comentarios;
                presupuesto.ProximoContacto = tratativa.Prox_Contacto;
                escribio =  _crmDbContext.SaveChanges();
            }

            if (escribio > 0)
                return true;
            else
                return false;

        }

        public async Task<IEnumerable<TransaccionGuardada>> ListadoDePresupuestosAsync(string sucursal, int vendedor, int dias, string estados)
        {
            Sucursal contexto = await credencialesAsync(sucursal);
            List<TransaccionGuardada> encontrados = new List<TransaccionGuardada>();
            List<PresupuestoDeConsulta> listaDevuelta = new List<PresupuestoDeConsulta>();
            var fechaDeBusqueda = DateTime.Today.AddDays(dias * -1);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                if(vendedor <= 0)   //si el vendedor viene en 0 devolver todo
                {
                    listaDevuelta = _crmDbContext.PresupuestosParaConsulta
                    .Where(i => i.IdEstadoDelPresupuesto == estados.ToUpper() && (i.FechaDeAlta.Date >= fechaDeBusqueda.Date && i.FechaDeAlta.Date <= DateTime.Today.Date))
                    .ToList();

                }else
                {
                    listaDevuelta = _crmDbContext.PresupuestosParaConsulta
                    .Where(i => i.IdEstadoDelPresupuesto == estados.ToUpper() && (i.FechaDeAlta.Date >= fechaDeBusqueda.Date && i.FechaDeAlta.Date <= DateTime.Today.Date) && i.IdVendedor == vendedor)
                    .ToList();
                }

                //una vez obtenida la lista buscar todo
                if(listaDevuelta.Count > 0)
                {
                    foreach (var item in listaDevuelta)
                    {
                        TransaccionGuardada nueva = new TransaccionGuardada();
                        nueva.Cliente = _crmDbContext.ClientesDeConsulta.Where(i => i.IdCliente == item.IdCliente).FirstOrDefault();
                        nueva.Presupuesto = item;
                        nueva.Detalles = _crmDbContext.DetallesParaConsulta.Where(p => p.IdPresupuesto == item.IdPresupuesto).ToList();
                        nueva.Direcciones = _crmDbContext.DireccionDeEntregaParaConsulta.Where(d => d.IdCliente == item.IdCliente).ToList();
                        encontrados.Add(nueva);
                    }
                }

            }

            return encontrados;

        }

        public async Task<ClienteDeConsulta> ObtenerClienteParaSeguimiento(string sucursal, int idCliente)
        {
            Sucursal contexto = await credencialesAsync(sucursal);
            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return _crmDbContext.ClientesDeConsulta.Where(i => i.IdCliente == idCliente).FirstOrDefault();

            }
            
        }


    }
}
