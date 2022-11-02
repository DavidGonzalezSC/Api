using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IEscrituraCRM
    {
        public void EscribirLogs(string sucursal, string mensaje);
        public Task<Transaccion> GuardarTransaccionAsyncV2(Transaccion transac, bool PasarApedido);
        public Task<Transaccion> EscribirSoloEnTemporalParaPruebas(Transaccion transac);
        public Task<TransaccionEscrituraDomicilios> EscribirSoloDomicilios(TransaccionEscrituraDomicilios direcciones);
        public Cliente validarCliente(Cliente cliente);
        public Presupuesto validarCabecera(Presupuesto presupuesto);
        public Task<IEnumerable<string>> validarDetalle(Detalle detalle);
        public Task<IEnumerable<string>> validarDomicDeEntrega(DireccionDeEntrega direccion);
        public Task<IEnumerable<string>> validarTransaccion(Transaccion transaccion);

        //--Pedidos

        //Validar Pedido
        public Task<ValidacionesPedido> validarPedido(Transaccion transac, CrmContexto contexto);



        //Anular
        public Task<bool> EliminarPedido(AnularpedidoDto anularDto);


        //--Escribir en Nexo
        public Task<IEnumerable<SqlRespuestaNexo>> EscribirEnNexoSPAsync(List<NexoEscrituraDTO> listadoaFacturar);
    }
}
