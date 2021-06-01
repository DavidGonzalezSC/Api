using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    public class Sucursal
    {
        [Key]
        public int IdConexion { get; set; }
        public string Nombre { get; set; }
        public string Servidor { get; set; }
        public string Base { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string Codigo { get; set; }
    }
}
