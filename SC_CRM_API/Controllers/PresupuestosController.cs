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
    public class PresupuestosController : Controller
    {

        private readonly IPresupuestos _presupuestos;

        public PresupuestosController(IMensajeria mensajeria, IPresupuestos presupuestos)
        {
            _presupuestos = presupuestos ?? throw new ArgumentNullException(nameof(IPresupuestos));
        }

        [HttpPost("seguimientos")]
        public async Task<IActionResult> obtenerCabecerasPorFechas([FromBody] SeguimientoPresupuestosDto parametros)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var ObtenerPresupuestos = await _presupuestos.ListadoDePresupuestosAsync(parametros.Sucursal, parametros.Vendedor, parametros.Dias, parametros.Estado);

            if (ObtenerPresupuestos.Count() > 0)
                return Ok(ObtenerPresupuestos);
            else
                return Ok();
        }

        [HttpGet("{sucursal}/seguimientos/{IdCliente}")]
        public async Task<IActionResult> obtenerCabecerasPorFechas([FromRoute] string sucursal, [FromRoute] int IdCliente)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var ObtenerCliente = await _presupuestos.ObtenerClienteParaSeguimiento(sucursal, IdCliente);

            if (ObtenerCliente != null)
                return Ok(ObtenerCliente);
            else
                return NotFound();
        }

    }
}
