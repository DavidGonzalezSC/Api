using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class SeguimientoPresupuestosDto
    {
        public string Sucursal { get; set; }
        public int Vendedor { get; set; }
        public int Dias { get; set; }
        public string Estado { get; set; }
    }
}
