using Microsoft.AspNetCore.Mvc;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices.ComTypes;

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

        [HttpPost("generarPdf")] //--LISTO
        public async Task<IActionResult> generarPdf([FromBody] MailDto datos)
        {
            var verificarSalida = await _mensajeria.confeccionarPdf(datos);
            return Ok(verificarSalida);

        }

        [HttpGet("descargar/{guid}")]
        public IActionResult DescargarPdf([FromRoute] string guid)
        {
            string archivo = AppDomain.CurrentDomain.BaseDirectory + $"\\Comandos\\Pdfs\\{guid}.pdf";
            if(System.IO.File.Exists(archivo))
            {
                Response.Headers.Add("Content-Disposition", "inline; filename=Presupuesto.pdf");
                return PhysicalFile(archivo, "application/pdf");

            }
            else
            {
                return NotFound();
            }


        }



    }
}
