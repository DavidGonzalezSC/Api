using System;
using System.Collections.Generic;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{

    public class SqlRespuestaNexo
    {
        public int Respuesta { get; set; }
    }


    public class SqlRespuesta
    {

        public string Resultado { get; set; }
        public DateTime Fecha { get; set; }
        public string Comprobante { get; set; }
        public string Error_Mensaje { get; set; }
        public string Error_Severidad { get; set; }
        public string Error_Estado { get; set; }
    }

    public class SqlRespuestaDomicilios
    {
        public string Resultado { get; set; }
        public DateTime Fecha { get; set; }
        public string Comprobante { get; set; }
        public string Error_Mensaje { get; set; }
        public string Error_Severidad { get; set; }
        public string Error_Estado { get; set; }
        public string NombreDomicilio { get; set; }
    }

    public class SqlRespuestaPlana
    {
        public string Resultado { get; set; }
    }

    public class SqlRespuestaEscribirRemito
    {
        public string nro_remito { get; set; }
    }

}
