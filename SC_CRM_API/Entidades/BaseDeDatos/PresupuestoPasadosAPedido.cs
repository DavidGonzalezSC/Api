using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("VW_ListarPedidosDePresupuestos")]
    public class PresupuestoPasadosAPedido
    {
        public int Id_Presupuesto { get; set; }
        public Int16 Talon_Ped { get; set; }
        public string Nro_Pedido { get; set; }
        public Int16 Estado { get; set; }
        public string TieneFacturas { get; set; }
    }
}
