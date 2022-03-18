using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{

    public class EstatusMagento
    {
        public int Orden { get; set; }
        public string Estado { get; set; }
        public string Procesado { get; set; }
        public string NroOrden { get; set; }
    }



    [Table("WS_MagentoOrdenes")]
    public class EstatusMagentoV1 :EstatusMagento
    {
        [Key]
        public int idMagentoOrden { get; set; }
        public DateTime AudIngresado { get; set; }
        public DateTime? AudProcesado { get; set; }

    }

    [Table("WS_MagentoOrdenes_V2")]
    public class EstatusMagentoV2 : EstatusMagento
    {
        [Key]
        public int idMagentoOrden { get; set; }
        public DateTime AudIngresado { get; set; }
        public DateTime? AudProcesado { get; set; }
        public string TransaccionMagento { get; set; }
        public string ProcesadoPor { get; set; }
        public string Pago_1 { get; set; }
        public string Pago_2 { get; set; }
        public string GatewayDePago { get; set; }
        public bool? Preprocesado { get; set; }


    }


}
