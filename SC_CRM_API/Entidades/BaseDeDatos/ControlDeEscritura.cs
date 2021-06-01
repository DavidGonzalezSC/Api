using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    public class ControlDeEscritura
    {
        public bool escrito { get; set; } = false;
        public IEnumerable<string> ErroresDeValidacion { get; set; } = new List<string>();
    }
}
