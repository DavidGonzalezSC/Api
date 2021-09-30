using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class ValidacionesPedido
    {
        public bool PedidoValido { get; set; }
        public List<string> ListaDeErrores = new List<string>();
    }
}
