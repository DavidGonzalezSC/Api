using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Entidades.BaseDeDatos;

namespace SC_CRM_API.Contextos
{
    public class AuxiliarContexto : DbContext
    {
        public DbSet<Meli_Auxiliar_V2> DbMeliV2 { get; set; }

        //Para actualizar estados
        public AuxiliarContexto(DbContextOptions<AuxiliarContexto> opciones)
            : base(opciones)
        {
        }
    }
}
