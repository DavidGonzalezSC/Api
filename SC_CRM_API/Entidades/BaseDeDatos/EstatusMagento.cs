using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("WS_MagentoOrdenes")]
    public class EstatusMagento
    {
        [Key]
        public int idMagentoOrden { get; set; }

        public int Orden { get; set; }
        public string Estado { get; set; }
        public string Procesado { get; set; }
        public DateTime? AudIngresado { get; set; }
        public DateTime? AudProcesado { get; set; }
        public string NroOrden { get; set; }

    }
}
