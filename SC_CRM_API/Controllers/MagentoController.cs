using Microsoft.AspNetCore.Mvc;
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

        public MagentoController(IMagento magento)
        {
            _magento = magento;
        }

        //--Busquedas Referentes a Clientes
        [HttpGet("sinProceso")] //--LISTO
        public async Task<IActionResult> ordenesSinProceso()
        {
            var listado =await _magento.OrdenesSinProcesar();
            return Ok(listado);

        }



    }
}
