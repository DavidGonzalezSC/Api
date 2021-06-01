using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IConsultasCRM
    {
        //-- CLIENTES --

        Task<bool> clienteExisteAsync(string sucursal, string cuit);        //listo
        Task<ClienteDeConsulta> buscarClientePorCuitAsync(string sucursal, string cuit);        //listo
        Task<ClienteDeConsulta> buscarClientePorId(string sucursal, string Id);        //listo
        Task<ClienteDeConsulta> buscarClientePorRazonSocialAsync(string sucursal, string RazonSocial); //--listo
        Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialTodosAsync(string sucursal, string cadenaDeBusqueda); //--Listo
        Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialQueComienzanConAsync(string sucursal, string cadenaDeBusqueda); //--Listo


        //--TODO en clientes
        //Cliente + ultimo presupuesto
        //Cliente + todos los presu
        //



        //--- PRESUPUESTOS -----
        //busqueda por numero
        //busqueda por fecha
        //por cliente






    }
}
