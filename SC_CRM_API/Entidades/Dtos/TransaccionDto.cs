using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class TransaccionDto 
    {
        public bool PasarAPedido { get; set; }
        public ClienteDto Cliente { get; set; } = new ClienteDto();
        public PresupuestoDto Presupuesto { get; set; } = new PresupuestoDto();
        public List<DetalleDto> DetallesDto { get; set; } = new List<DetalleDto>();
        public List<DireccionDeEntregaDto> DireccionDeEntrega { get; set; } = new List<DireccionDeEntregaDto>();
    }

    public class TransaccionDomiciliosDto
    {
        public int IdDeCliente { get; set; }
        public List<DireccionDeEntregaDto> DireccionDeEntrega { get; set; } = new List<DireccionDeEntregaDto>();
    }
}
