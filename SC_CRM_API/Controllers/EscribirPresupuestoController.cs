using Microsoft.AspNetCore.Mvc;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Controllers
{
    [Route("api/[controller]")]
    public class EscribirPresupuestoController : ControllerBase
    {
        private readonly IEscrituraCRM _escritura;

        public EscribirPresupuestoController(IEscrituraCRM escritura)
        {
            _escritura = escritura;
        }


        [HttpPost("{cadena}/validarCliente")] //--LISTO
        public async Task<IActionResult> validarCliente([FromRoute] string cadena, [FromBody] ClienteDto cliente)
        {
            var verificarCliente = await _escritura.validarCliente(cliente);
            return Ok(verificarCliente);

        }

        [HttpPost("{cadena}/validarCabecera")] //--LISTO
        public async Task<IActionResult> validarCabecera([FromRoute] string cadena, [FromBody] PresupuestoDto cabecera)
        {
            var verificarCabecera = await _escritura.validarCabecera(cabecera);
            return Ok(verificarCabecera);

        }

        [HttpPost("{cadena}/validarDetalle")] //--LISTO
        public async Task<IActionResult> validarRenglon([FromRoute] string cadena, [FromBody] DetalleDto detalle)
        {
            var verificarDetalle = await _escritura.validarDetalle(detalle);
            return Ok(verificarDetalle);

        }

        [HttpPost("{cadena}/validarDomicilio")] //--LISTO
        public async Task<IActionResult> validarDomicilio([FromRoute] string cadena, [FromBody] DireccionDeEntregaDto entrega)
        {
            var verificarDomEntrega = await _escritura.validarDomicDeEntrega(entrega);
            return Ok(verificarDomEntrega);

        }

        [HttpPost("{cadena}/validarTransaccion")] //--LISTO
        public async Task<IActionResult> validarTransaccion([FromRoute] string cadena, [FromBody] TransaccionDto transaccDto)
        {

            var transaccion = new Transaccion(cadena);
            transaccion.Cliente = transaccDto.Cliente;
            transaccion.Presupuesto = transaccDto.Presupuesto;

            foreach (DetalleDto item in transaccDto.DetallesDto)
            {
                transaccion.Detalles.Add(item);
            }
            
            transaccion.DireccionDeEntrega = transaccDto.DireccionDeEntrega;

            var verificarTransaccion = await _escritura.validarTransaccion(transaccion);
            return Ok(verificarTransaccion);

        }

        [HttpPost("{cadena}/escribirPedido")] //--LISTO
        public async Task<IActionResult> escribirPedido([FromRoute] string cadena, [FromBody] TransaccionDto transaccDto)
        {

            var transaccion = new Transaccion(cadena);
            transaccion.Cliente = transaccDto.Cliente;
            transaccion.Presupuesto = transaccDto.Presupuesto;

            foreach (DetalleDto item in transaccDto.DetallesDto)
            {
                transaccion.Detalles.Add(item);
            }

            transaccion.DireccionDeEntrega = transaccDto.DireccionDeEntrega;
            var escribio = await _escritura.GuardarTransaccionAsyncV2(transaccion);

            //--Verificar que pasó
            if (!escribio.EscrituraExitosa)
            {
                //--Bun
                string mensaje = "";
                //--JSONIFICAR
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(transaccion);
                //--Escribir un Log
                _escritura.EscribirLogs(cadena, json);


                if (escribio.ListaDeErrores.Any())
                {
                    return Ok(escribio.ListaDeErrores);

                }else
                {
                    if (!escribio.ClienteSave)
                        mensaje = "Cliente con Errores";
                    else if (!escribio.PresupuestoSave)
                        mensaje = "Presupuesto con Errores";
                    else if (!escribio.DomicEntregaSave)
                        mensaje = "Domicilio con Errores";
                    else if (!escribio.TangoSave)
                        mensaje = "Tango con Errores";
                }


                return Ok(mensaje);

            }else
            {
                return Ok(escribio.ListaDePedidos);
            }

        }


    }
}
