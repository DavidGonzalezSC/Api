using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class PresupuestoDevueltoDbDto
    {
        public string Sucursal { get; set; }
        public int NumeroPedido { get; set; }
        public Guid Identificador { get; set; } = Guid.NewGuid();
        public ClienteDeConsulta Cliente { get; set; } = new ClienteDeConsulta();
        public PresupuestoDeConsulta Presupuesto { get; set; } = new PresupuestoDeConsulta();
        public List<DetalleDeConsulta> DetallesDto { get; set; } = new List<DetalleDeConsulta>();
        public List<DireccionDeEntregaDeConsulta> DireccionesDeEntrega { get; set; } = new List<DireccionDeEntregaDeConsulta>();
    }
}
