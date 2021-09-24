using Microsoft.AspNetCore.Mvc;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : Controller
    {
        private readonly IPresupuestos _presupuestos;
        private readonly IEscrituraCRM _escritura;

        public PedidosController(IMensajeria mensajeria, IPresupuestos presupuestos, IEscrituraCRM escritura)
        {
            _presupuestos = presupuestos ?? throw new ArgumentNullException(nameof(IPresupuestos));
            _escritura = escritura ?? throw new ArgumentNullException(nameof(IEscrituraCRM));
        }

        [HttpPost("anular")]
        public async Task<IActionResult> eliminarPedido([FromBody] AnularpedidoDto parametros)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var anulo = await _escritura.EliminarPedido(parametros);
            return Ok(anulo);
        }
    }
}
