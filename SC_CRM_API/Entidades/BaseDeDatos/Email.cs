using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("MAIL_Generado_Para_Enviar")]
    public class Email
    {
        [Key]
        public int Id { get; set; }

        public int EventoID { get; set; }
        public DateTime TmeStamp { get; set; }
        public bool Suspendido { get; set; }
        public int De { get; set; }
        public string Para { get; set; }
        public string CC { get; set; }
        public string CCO { get; set; }
        public string Asunto { get; set; }
        public string Cuerpo { get; set; }
        [Column("Formato")]
        public bool HTML { get; set; }
        public int Prioridad { get; set; }
        public bool Procesado { get; set; }
        public string Usuario { get; set; }
        public DateTime? Enviado { get; set; }
    }
}
