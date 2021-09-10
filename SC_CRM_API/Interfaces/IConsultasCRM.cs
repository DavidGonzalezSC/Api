using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
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


        
        //--PRESUPUESTO
        Task<List<PresupuestoDeConsulta>> buscarPresupuestosPorCliente(string sucursal, string idCliente);
        Task<List<DireccionDeEntregaDeConsulta>> buscarDomiciliosPorCliente(string sucursal, string idCliente);
        Task<List<DetalleDeConsulta>> buscarDetallesPorPresupuesto(string sucursal, string idPresupuesto);
        Task<PresupuestoDevueltoDbDto> obtenerPresupuesto(string sucursal, string IdPresupuesto);
        //-- Todo: PRESUPUESTO POR NUMERO



        //--PEDIDO
        Task<PedidoDeConsultaDto> buscarPedido(string sucursal, Int16 Talonario, string NroDePedido);
       // pedidos por presu
         //   pedidos por cliente











    }
}
