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

    }
}
