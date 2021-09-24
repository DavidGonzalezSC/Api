using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class SeguimientosPresupuestoTratativa
    {
        public int Idpresupuesto { get; set; }
        public string Comentarios { get; set; }
        public DateTime Prox_Contacto { get; set; }

    }
}
