using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{

    public class NexoEscrituraDTO
    {
        public string CodigoSucursal { get; set; }
        public string nroPedido { get; set; } //@nroPedido VARCHAR(20)
        public int talonPed { get; set; } //@talonPed INT
        public string OrdenMagento { get; set; } //@OrdenMagento VARCHAR(30), 
        public string nroTransaccion { get; set; } //@nroTransaccion varchar(40)
        public int id_sba22 { get; set; } //@id_sba22 INT
        public int? id_promocion_tarjeta { get; set; } //@id_promocion_tarjeta
        public int cantidadCuotas { get; set; } //@cantidadCuotas int,
        public int id_sba21 { get; set; } //@id_sba21 int
    }
}
