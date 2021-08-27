using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class MailDto
    {
        public string Sucursal { get; set; }
        public int Presupuesto { get; set; }
        public string Plantilla { get; set; }
    }
}
