using Microsoft.AspNetCore.Http;
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
    public class EscrituraSucursalMiscController : ControllerBase
    {
        private readonly IMiscelaneos _miscs;
        public EscrituraSucursalMiscController(IMiscelaneos miscelaneos)
        {
            _miscs = miscelaneos ?? throw new ArgumentNullException(nameof(IMiscelaneos));
        }

        [HttpGet("{cadena}/sinPresupuesto/{dias}")] //--LISTO
        public async Task<IActionResult> listaSinPresupuesto([FromRoute] string cadena, [FromRoute] string dias)
        {

            int NDias = Convert.ToInt32(dias);

            var retorno = await _miscs.listadoSeguimientosSinPresupuesto(cadena, NDias);
            if(retorno.Count > 0)
            {
                var listaDto =  retorno.Select(e => new {e.ID_Tratativa, e.Nombre, e.Comentarios, e.Fecha }).ToList();
                return Ok(listaDto);
            }else
            {
                return Ok();
            }

        }

        [HttpPost("{sucursal}/sinPresupuesto/guardar")]
        public async Task<IActionResult> guardarDatosTratativas([FromRoute] string sucursal, [FromBody] SeguimientoSinPresupuestoDto tratativa)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var guardarDatos = await _miscs.guardarContactoSinPresupuestoNuevo(sucursal, tratativa);
            return Ok(guardarDatos);
        }

        [HttpPost("{sucursal}/sinPresupuesto/actualizar")]
        public async Task<IActionResult> actualizarDatosTratativas([FromRoute] string sucursal, [FromBody] SeguimientoSinPresupuestoDto tratativa)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var guardarDatos = await _miscs.editarContactoSinPresupuestoNuevo(sucursal, tratativa);
            return Ok(guardarDatos);
        }



    }
}
