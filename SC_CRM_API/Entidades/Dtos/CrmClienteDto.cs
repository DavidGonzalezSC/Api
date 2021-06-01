using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class CrmClienteDto
    {
        public int Id { get; set; }
        public string Cuit { get; set; }
        public string RazonSocial { get; set; }
    }
}
