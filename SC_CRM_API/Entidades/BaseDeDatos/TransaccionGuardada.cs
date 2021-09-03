using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    public class TransaccionGuardada
    {
        public ClienteDeConsulta Cliente { get; set; }
        public PresupuestoDeConsulta Presupuesto { get; set; }
        public IEnumerable<DetalleDeConsulta> Detalles { get; set; } = new List<DetalleDeConsulta>();
        public IEnumerable<DireccionDeEntregaDeConsulta> Direcciones { get; set; } = new List<DireccionDeEntregaDeConsulta>();
    }
}
