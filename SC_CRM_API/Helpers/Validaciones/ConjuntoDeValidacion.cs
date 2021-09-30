using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Helpers.Validaciones
{
    public class ConjuntoDeValidacion
    {
        public List<ValidarInactivos> ListaDeInactivos = new List<ValidarInactivos>();
        public List<ValidarExcepcionComercial> ListaDeExcepcionesComerciales = new List<ValidarExcepcionComercial>();
        public List<ValidarArticuloProveedor> ListaDeArticulosPorProveedor = new List<ValidarArticuloProveedor>();
        public List<Transportes> ListaDeTransportes = new List<Transportes>();
        public List<SucursalesValidarDepoTransporte> ListaDeDepoTransporte = new List<SucursalesValidarDepoTransporte>();
        public List<ValidacionesSPConsulta> ListaSpConsulta = new List<ValidacionesSPConsulta>();
        public List<ValidacionesSPBloqueantes> ListaSpBloqueantes = new List<ValidacionesSPBloqueantes>();
        public List<ValidacionesDepoDeFabricantes> ListaFabricantes = new List<ValidacionesDepoDeFabricantes>();

    }

    public class ValidarInactivos
    {
        public string CodArticu { get; set; }
        public string Deposito { get; set; }
    }

    public class ValidarExcepcionComercial
    {
        public string Clasificacion { get; set; }
        public string Descripcion { get; set; }
    }

    public class ValidarArticuloProveedor
    {
        public string CodigoArticulo { get; set; }
        public string CodigoProveedor { get; set; }
        public string Deposito { get; set; }
    }

    public class Transportes
    {
        public int Id { get; set; }
        public string CodigoTransporte { get; set; }
        public string Nombre { get; set; }
    }

    public class SucursalesValidarDepoTransporte
    {
        public int Id { get; set; }
        public string deposito { get; set; }
        public string transporte { get; set; }
    }

    public class ValidacionesSPConsulta
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    
    public class ValidacionesSPBloqueantes
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
    }

    public class ValidacionesDepoDeFabricantes
    {
        public string Deposito { get; set; }
        public string Fabrica { get; set; }
    }
}
