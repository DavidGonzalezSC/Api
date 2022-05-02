using Microsoft.AspNetCore.Mvc;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Controllers
{
    [Route("api/[controller]")]
    public class BusquedasController : ControllerBase
    {
        private readonly IConsultasCRM _consultas;

        public BusquedasController(IConsultasCRM consultas)
        {
            _consultas = consultas;
        }

        //--Busquedas Referentes a Clientes
        [HttpGet("{sucursal}/clienteExiste/{cuit}")] //--LISTO
        public async Task<IActionResult> clienteExiste([FromRoute] string sucursal, [FromRoute] string cuit)
        {
            var verificarCliente = await _consultas.clienteExisteAsync(sucursal, cuit);
            return Ok(verificarCliente);

        }

        [HttpGet("{sucursal}/clienteporCuit/{cuit}")] //--LISTO
        public async Task<IActionResult> clientePorCuit([FromRoute] string sucursal, [FromRoute] string cuit)
        {
            var clientePorCuit = await _consultas.buscarClientePorCuitAsync(sucursal, cuit);
            return Ok(clientePorCuit);

        }

        [HttpGet("{sucursal}/clienteporId/{Id}")] //--LISTO
        public async Task<IActionResult> clientePorId([FromRoute] string sucursal, [FromRoute] string Id)
        {
            var clientePorCuit = await _consultas.buscarClientePorId(sucursal, Id);
            return Ok(clientePorCuit);

        }

        [HttpGet("{sucursal}/clienteporcodigoTango/{tango}")] //--LISTO
        public async Task<IActionResult> clientePorTango([FromRoute] string sucursal, [FromRoute] string tango)
        {
            var clientePorCuit = await _consultas.buscarClientePorTango(sucursal, tango);
            return Ok(clientePorCuit);

        }



        [HttpGet("{sucursal}/clientePorNombre/{razonSocial}")] //--LISTO
        public async Task<IActionResult> clientePorRazonSocial([FromRoute] string sucursal, [FromRoute] string razonSocial)
        {
            if (razonSocial.Contains("\'"))
            {
                return Forbid();
            }

            var clientePorNombre = await _consultas.buscarClientePorRazonSocialAsync(sucursal, razonSocial);
            return Ok(clientePorNombre);

        }



        [HttpGet("{sucursal}/listaclientesPorNombre/{busqueda}")] //--LISTO
        public async Task<IActionResult> listadoclientePorRazonSocial([FromRoute] string sucursal, [FromRoute] string busqueda)
        {
            if (busqueda.Contains("\'"))
            {
                return Forbid();
            }
            var clientePorNombre = await _consultas.buscarClientePorRazonSocialTodosAsync(sucursal, busqueda);

            if(clientePorNombre.Count() > 1)
            {
                var listaAnonima = new List<CrmClienteDto>();

                foreach (ClienteDeConsulta item in clientePorNombre)
                {
                    var pase = new CrmClienteDto
                    {
                        Id = item.IdCliente,
                        Cuit = item.Cuit,
                        RazonSocial = item.RazonSocial
                    };
                    listaAnonima.Add(pase);
                }
                return Ok(listaAnonima);
            }else
            {
                return Ok(clientePorNombre);
            }

        }

        [HttpGet("{sucursal}/listaclientesPorNombreQueComienzanCon/{busqueda}")] //--LISTO
        public async Task<IActionResult> listadoclienteQueComienzanPorRazonSocial([FromRoute] string sucursal, [FromRoute] string busqueda)
        {
            var clientePorNombre = await _consultas.buscarClientePorRazonSocialQueComienzanConAsync(sucursal, busqueda);
            return Ok(clientePorNombre);

        }

        //--Busquedas de Presupuestos
        [HttpGet("{sucursal}/listaPresupuestosPorClienteId/{Id}")] //--
        public async Task<IActionResult> listaDeCabeceraDePresupuestoPorCliente([FromRoute] string sucursal, [FromRoute] string Id)
        {
            var presupuestos = await _consultas.buscarPresupuestosPorCliente(sucursal, Id);
            return Ok(presupuestos);
        }

        //--Busquedas de Direcciones
        [HttpGet("{sucursal}/listaDireccionesPorClienteId/{Id}")] //--
        public async Task<IActionResult> listaDeDireccionesPorCliente([FromRoute] string sucursal, [FromRoute] string Id)
        {
            var direcciones = await _consultas.buscarDomiciliosPorCliente(sucursal, Id);
              return Ok(direcciones);

        }


        //--Busquedas de Renglones de Detalles
        [HttpGet("{sucursal}/renglonesDelpresupuesto/{IdPresupuesto}")] //--
        public async Task<IActionResult> renglonesDelPresupuesto([FromRoute] string sucursal, [FromRoute] string IdPresupuesto)
        {
            var renglones = await _consultas.buscarDetallesPorPresupuestoVista(sucursal, IdPresupuesto);
            //var renglones = await _consultas.buscarDetallesPorPresupuesto(sucursal, IdPresupuesto);
            if (renglones.Count() > 0)
                return Ok(renglones);
            else
                return NotFound();
        }

        //-- endpoiunt que traiga cvabecera y detalle segun Id y Numero
        [HttpGet("{sucursal}/presupuesto/{IdPresupuesto}")]
        public async Task<IActionResult> obtenerPresupuesto([FromRoute] string sucursal, [FromRoute] string IdPresupuesto)
        {
            var presupuesto = await _consultas.obtenerPresupuesto(sucursal, IdPresupuesto);
            if (presupuesto != null)
                return Ok(presupuesto);
            else
                return NotFound();

        }

        //-- endpoiunt que traiga el presupedido segun magento
        [HttpGet("{sucursal}/presupuestoPorMagento/{IdMagento}")]
        public async Task<IActionResult> obtenerPresupuestoPorMagento([FromRoute] string sucursal, [FromRoute] string IdMagento)
        {
            var presupuesto = await _consultas.obtenerPresupuestoPorMagento(sucursal, IdMagento);
            if (presupuesto != null)
                return Ok(presupuesto);
            else
                return NotFound();

        }

        //-- endpoiunt que traiga segun pedido
        [HttpGet("{sucursal}/presupuestoPorPedido/{NroPedido}")]
        public async Task<IActionResult> obtenerPresupuestoPorPedido([FromRoute] string sucursal, [FromRoute] string NroPedido)
        {
            var presupuesto = await _consultas.obtenerPresupuestoPorPedido(sucursal, NroPedido);
            if (presupuesto != null)
                return Ok(presupuesto);
            else
                return NotFound();

        }


        [HttpGet("{sucursal}/presupuestocabecera/{IdPresupuesto}")]
        public async Task<IActionResult> obtenerCabecera([FromRoute] string sucursal, [FromRoute] string IdPresupuesto)
        {
            var presupuesto = await _consultas.obtenerPresupuesto(sucursal, IdPresupuesto);
            if (presupuesto != null)
                return Ok(presupuesto.Presupuesto);
            else
                return NotFound();

        }

        //--PEDIDOS
        
        [HttpGet("{sucursal}/pedidos/{Talonario}/{NroPedido}")]
        public async Task<IActionResult> obtenerPedido([FromRoute] string sucursal, [FromRoute] Int16 Talonario, [FromRoute] string NroPedido)
        {
            var pedido = await _consultas.buscarPedido(sucursal,Talonario, NroPedido);

            if (pedido.Cabecera.TalonPedido == 0 && string.IsNullOrEmpty(pedido.Cabecera.Nro_Pedido))
                return NotFound();
            else
                return Ok(pedido);
        }

        [HttpGet("{sucursal}/obtenercabecerapedidos/{Talonario}/{NroPedido}")]
        public async Task<IActionResult> obtenerDetallesDePedido([FromRoute] string sucursal, [FromRoute] Int16 Talonario, [FromRoute] string NroPedido)
        {

            var pedido = await _consultas.buscarPedidoConDetalle(sucursal, Talonario, NroPedido);
            return Ok(pedido);
        }

        [HttpGet("{sucursal}/listarPdos/{PresuId}")]
        public async Task<IActionResult> listarPedidosDelPresu([FromRoute] string sucursal, [FromRoute] string PresuId)
        {
            var listado = await _consultas.listadoPresupuestoPasadosAPedido(sucursal, PresuId);
            return Ok(listado);
        }



        //--Presupuestos va en otro controlador por separado



    }
}
