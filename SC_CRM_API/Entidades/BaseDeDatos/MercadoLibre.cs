using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    public class MercadoLibre
    {
    }

    [Table("GVA10")]
    public class ListaDePreciosMeli
    {
        [Key]
        public int Id_Gva10 { get; set; }
        public bool Habilitada { get; set; }
        public string Nombre_Lis { get; set; }
        public Int16 NRO_DE_LIS { get; set; }
    }

    [Table("WS_MercadoLibreOrdenes_V2")]
    public class Meli_Auxiliar_V2
    {
        [Key]
        public int idInterno { get; set; }
        public string IdOrdenMeli { get; set; }
        public string IdEnvioMeli { get; set; }
        public string Status { get; set; }
        public string Procesado { get; set; }
        public DateTime AudIngresado { get; set; }
        public string JsonFactura { get; set; }
        public string JsonOrden { get; set; }
        public string JsonEnvio { get; set; }
        public string Pedido { get; set; }
    }

    [Table("VW_Stock_Integrador_API")]
    public class MeliStock
    {
        [Column("COD_ARTICU")]
        public string Sku { get; set; }

        [Column("COD_DEP")]
        public string Deposito { get; set; }

        public int Cantidad { get; set; }
    }


    [Table("VW_ArticulosSimplesConProveedor")]
    public class ProveedorArtSimple //--vive solo en thames
    {
        public string Sku { get; set; }
        public string Proveedor { get; set; }
        public int? TiempoFabricacion { get; set; }
    }










}
