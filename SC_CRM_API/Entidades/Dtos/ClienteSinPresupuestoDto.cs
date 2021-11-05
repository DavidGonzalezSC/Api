using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
  public class ClienteSinPresupuestoDto
    {
        public int IdVendedor { get; set; }
        public DateTime Fecha { get; set; }
        public int Cantidad { get; set; }


    }
}
