using System;
using System.Collections.Generic;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    public class SqlRespuesta
    {

        public string Resultado { get; set; }
        public DateTime Fecha { get; set; }
        public string Comprobante { get; set; }
        public string Error_Mensaje { get; set; }
        public string Error_Severidad { get; set; }
        public string Error_Estado { get; set; }
    }
}
