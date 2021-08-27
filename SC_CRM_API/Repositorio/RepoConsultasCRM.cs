using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoConsultasCRM : IConsultasCRM
    {

        private readonly IServiciosSucursales _sucursales;

        public RepoConsultasCRM(IServiciosSucursales sucursales)
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

        public async Task<ClienteDeConsulta> buscarClientePorRazonSocialAsync(string sucursal, string RazonSocial)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.RazonSocial == RazonSocial.Trim()).FirstOrDefault();

            }

        }

        public async Task<ClienteDeConsulta> buscarClientePorCuitAsync(string sucursal, string cuit)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.Cuit == cuit.Trim()).FirstOrDefault();

            }

        }

        public async Task<bool> clienteExisteAsync(string sucursal, string cuit)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.ClientesDeConsulta.Any(c => c.Cuit == cuit.Trim());

            }
        }

        public async Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialTodosAsync(string sucursal, string cadenaDeBusqueda)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                //var lista = _crmDbContext.ClientesDeConsulta.Where(c => c.RazonSocial.Contains(cadenaDeBusqueda)).ToListAsync();
                var lista = _crmDbContext.ClientesDeConsulta.FromSqlRaw($"select * from CRM_CLIENTES where UPPER(RazonSocial) like '%{cadenaDeBusqueda.ToUpper().Trim()}%'").ToListAsync();
                return lista.Result.OrderBy(r => r.RazonSocial);

            }
        }

        public async Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialQueComienzanConAsync(string sucursal, string cadenaDeBusqueda)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                var lista = _crmDbContext.ClientesDeConsulta.Where(c => c.RazonSocial.StartsWith(cadenaDeBusqueda)).ToListAsync();
                return lista.Result.OrderBy(r => r.RazonSocial);

            }
        }

        public async Task<ClienteDeConsulta> buscarClientePorId(string sucursal, string Id)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == Convert.ToInt32(Id.Trim())).FirstOrDefault();

            }
        }
    }
}
