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
    public class MensajesController : Controller
    {
        private readonly IMensajeria _mensajeria;
        public MensajesController(IMensajeria mensajeria)
        {
            _mensajeria = mensajeria ?? throw new ArgumentNullException(nameof(IMensajeria));
        }

        [HttpGet("plantillas")]
        public IActionResult Listarpalntillas()
        {
            var retorno = _mensajeria.ListarPlantillas();
            return Ok(retorno);
        }

        [HttpPost("enviarMail")] //--LISTO
        public async Task<IActionResult> enviarPorMail([FromBody] MailDto datos)
        {
            var verificarSalida = await _mensajeria.confeccionarMail(datos);
            if (verificarSalida)
                return Ok("Enviado");
            else
                return Ok("Bun");

        }


    }
}
