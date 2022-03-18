using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Hubs;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MagentoController : Controller
    {
        private readonly IMagento _magento;
        private readonly IHubContext<HubMagento> _hubContext;

        public MagentoController(IMagento magento, IHubContext<HubMagento> hubContext)
        {
            _magento = magento;
            _hubContext = hubContext;
        }


        
        [HttpGet("sinProcesoV1")] //--LISTO
        public async Task<IActionResult> ordenesSinProcesoV1()
        {
            //Descontinuar cuando V2 entre a implementar
            var listado = await _magento.OrdenesSinProcesarV1();
            await _hubContext.Clients.All.SendAsync("ObtenerOrdenesV1", listado);
            return Ok(listado);

        }

        [HttpGet("ultimas24hV1")] //--LISTO
        public async Task<IActionResult> ordenesUltimas24hV1()
        {
            //Descontinuar cuando V2 entre a implementar
            var listado = await _magento.OrdenesDeUltimas24HorasV1();
            await _hubContext.Clients.All.SendAsync("ultimas24hv1", listado);
            return Ok(); //

        }


        [HttpGet("sinProcesoV2")] //--LISTO
        public async Task<IActionResult> ordenesSinProcesoV2()
        {
            //-- a este endpoint le paga el WS de informar ordenes que vive en el 05 cada vez que escribe en tabla
            //var listado =await _magento.OrdenesSinProcesarV1();
            var listado = await _magento.OrdenesSinProcesarV2();
            var listado10 = listado.OrderByDescending(r => r.idMagentoOrden).Take(10);
            await _hubContext.Clients.All.SendAsync("ObtenerOrdenesV2", listado10);
            return Ok(listado10);

        }

        //--Procesar Transacciones y completar datos de magento y gateway de pago
        [HttpGet("procesar/{numeroDeOrden}")] //--LISTO
        public async Task<IActionResult> RecolectarDatos([FromRoute] string numeroDeOrden)
        {
            var resultado = await _magento.ObtenerDatosExternos(numeroDeOrden);
            return Ok(resultado);

        }



        [HttpPost("nuevaOrden")]
        public async Task<IActionResult> EscrituraSimpleV1MagentoAsync([FromBody] EstatusMagentoV1 magento)
        {
            var ordenescrita = await _magento.EscribirOrdenV1(magento);
            if (ordenescrita > 0)
            {
                var listado = await _magento.OrdenesSinProcesarV1();
                await _hubContext.Clients.All.SendAsync("ObtenerOrdenes", listado.OrderByDescending(r => r.idMagentoOrden).Take(10));
                return Ok();

            }
            else
                return BadRequest();
        }


    }
}
