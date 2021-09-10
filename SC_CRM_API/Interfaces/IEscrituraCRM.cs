using FluentValidation.Results;
using SC_CRM_API.Entidades.BaseDeDatos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IEscrituraCRM
    {
        public void EscribirLogs(string sucursal, string mensaje);
        public Task<Transaccion> GuardarTransaccionAsyncV2(Transaccion transac, bool PasarApedido);
        public Task<Transaccion> EscribirSoloEnTemporalParaPruebas(Transaccion transac);
        public Task<IEnumerable<string>> validarCliente(Cliente cliente);
        public Task<IEnumerable<string>> validarCabecera(Presupuesto presupuesto);
        public Task<IEnumerable<string>> validarDetalle(Detalle detalle);
        public Task<IEnumerable<string>> validarDomicDeEntrega(DireccionDeEntrega direccion);
        public Task<IEnumerable<string>> validarTransaccion(Transaccion transaccion);
    }
}
