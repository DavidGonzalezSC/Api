using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("VW_CRM_Tratativas")]
    public class TratativaSinPResupuesto
    {

        [Key]
        public int ID_Tratativa { get; set; }
        public int ID_Vendedor { get; set; }
        public string Nombre { get; set; }
        public string Comentarios { get; set; }
        public DateTime Fecha { get; set; }
    }
}
