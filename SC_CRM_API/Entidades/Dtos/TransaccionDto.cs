using SC_CRM_API.Entidades.BaseDeDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class TransaccionDto 
    {
        public ClienteDto Cliente { get; set; } = new ClienteDto();
        public PresupuestoDto Presupuesto { get; set; } = new PresupuestoDto();
        public List<DetalleDto> DetallesDto { get; set; } = new List<DetalleDto>();
        public DireccionDeEntregaDto DireccionDeEntrega { get; set; } = new DireccionDeEntregaDto();
    }
}
