using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class AnularpedidoDto
    {
        public string Sucursal { get; set; }
        public Int16 Talonario { get; set; }
        public string Nro_Pedido { get; set; }
    }
}
