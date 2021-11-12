using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoMiscelaneo : IMiscelaneos
    {
        private readonly IServiciosSucursales _sucursales;

        public RepoMiscelaneo(IServiciosSucursales sucursales)
        {
            _sucursales = sucursales ?? throw new ArgumentNullException(nameof(IServiciosSucursales));
        }

        public async Task<Sucursal> credencialesAsync(string sucursal)
        {
            Sucursal credenciales = await _sucursales.Sucursal(sucursal);

            if (credenciales != null)
                return credenciales;
            else
                return null;
        }

        public async Task<bool> editarContactoSinPresupuestoNuevo(string sucursal, TratativaSinPResupuesto datos)
        {
            Sucursal contexto = await credencialesAsync(sucursal);
            bool escritura = false;

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                var spEscribeCliente = new SqlRespuesta();

                try
                {
                    spEscribeCliente = _crmDbContext.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_CRM_TratativasActualizar" +
                        $"'{datos.ID_Tratativa}', '{datos.ID_Vendedor}', '{datos.Comentarios}', '{datos.Fecha}', {datos.ID_Vendedor};").AsEnumerable().FirstOrDefault();

                    escritura = true;
                }
                catch (Exception error)
                {
                    spEscribeCliente = new SqlRespuesta
                    {
                        Resultado = "Catch",
                        Comprobante = "",
                        Fecha = DateTime.Now,
                        Error_Mensaje = error.Message,
                        Error_Severidad = "0",
                        Error_Estado = "0"
                    };
                    
                }

                return escritura;

            }
        }

        public async Task<bool> guardarContactoSinPresupuestoNuevo(string sucursal, TratativaSinPResupuesto datos)
        {
            Sucursal contexto = await credencialesAsync(sucursal);
            bool escritura = false;

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                var spEscribeCliente = new SqlRespuesta();

                try
                {
                    spEscribeCliente = _crmDbContext.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_CRM_TratativasInsertar" +
                        $"'{datos.ID_Vendedor}', '{datos.Comentarios}', '{datos.Fecha}', {datos.ID_Vendedor};").AsEnumerable().FirstOrDefault();

                    escritura = true;
                }
                catch (Exception error)
                {
                    spEscribeCliente = new SqlRespuesta
                    {
                        Resultado = "Catch",
                        Comprobante = "",
                        Fecha = DateTime.Now,
                        Error_Mensaje = error.Message,
                        Error_Severidad = "0",
                        Error_Estado = "0"
                    };

                }

                return escritura;

            }
        }

        public async Task<List<TratativaSinPResupuesto>> listadoSeguimientosSinPresupuesto(string sucursal, int dias)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            DateTime fecha = DateTime.Now.AddDays(dias * -1);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return  _crmDbContext.TratativasDeSeguimientosSin.Where(f => f.Fecha.Date >= fecha && f.Fecha <= DateTime.Now.Date)
                    .OrderBy(f => f.Fecha.Date).ToList();

            }
        }


    }
}
