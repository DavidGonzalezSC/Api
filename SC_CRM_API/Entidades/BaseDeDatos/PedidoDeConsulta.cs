using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("VW_CRM_Pedidos")]
    public class PedidoDeConsulta
    {
        [Column("TALON_PED")]
        public Int16 TalonPedido { get; set; }
        [Column("NRO_PEDIDO")]
        public string Nro_Pedido { get; set; }
        [Column("COD_CLIENT")]
        public string Cod_Cliente { get; set; }
        [Column("RazonSocial")]
        public string RazonSocial { get; set; }
        [Column("Telefono1")]
        public string Telefono1 { get; set; }
        [Column("Telefono2")]
        public string Telefono2 { get; set; }
        [Column("Domicilio_Cliente")]
        public string Domicilio { get; set; }
        [Column("CP_Cliente")]
        public string CodPostal { get; set; }
        [Column("Localidad_Cliente")]
        public string Localidad { get; set; }
        [Column("NOMBRE_VEN")]
        public string Vendedor { get; set; }
        [Column("NOMBRE_LIS")]
        public string Lista { get; set; }
        [Column("COD_SUCURS")]
        public string CodSucursal { get; set; }
        [Column("E_MAIL")]
        public string Email { get; set; }
        [Column("Fecha_Pedido")]
        public DateTime FechaPedido { get; set; }
        [Column("Clasificacion")]
        public string Clasificacion { get; set; }
        [Column("CUIT")]
        public string Cuit { get; set; }
        [Column("Fecha_Entrega")]
        public DateTime FechaDeEntrega { get; set; }
        [Column("LeyendaFechaEntrega")]
        public string LeyendaDeEntrega { get; set; }
        [Column("Comentarios")]
        public string Comentarios { get; set; }
        [Column("NOMBRE_TRA")]
        public string Transporte { get; set; }
        [Column("LEYENDA_1")]
        public string Leyenda_1 { get; set; }
        [Column("LEYENDA_2")]
        public string Leyenda_2 { get; set; }
        [Column("CA_RangoHorario")]
        public string RangoHorario { get; set; }
        [Column("Calle_Entrega")]
        public string Entrega_Calle { get; set; }
        [Column("NroDomicilio_Entrega")]
        public string Entrega_Numero { get; set; }
        [Column("Piso_Entrega")]
        public string Entrega_Piso { get; set; }
        [Column("Departamento_Entrega")]
        public string Entrega_Depto { get; set; }
        [Column("CP_Entrega")]
        public string Entrega_CP { get; set; }
        [Column("Localidad_Entrega")]
        public string Entrega_Localidad { get; set; }
        [Column("Provincia_Entrega")]
        public string Entrega_Provincia { get; set; }
        [Column("Nombre_Pais")]
        public string Entrega_Pais { get; set; }
        [Column("Renglon")]
        public int Renglon { get; set; }
        [Column("COD_ARTICU")]
        public string Cod_articulo { get; set; }
        [Column("DESCRIPCIO")]
        public string Descripcion { get; set; }
        [Column("DESC_ADIC")]
        public string Descripcion_Adicional { get; set; }
        [Column("Cantidad")]
        public int Cantidad { get; set; }
        [Column("Descuento")]
        public decimal Descuento { get; set; }
        [Column("PrecioUnitario")]
        public decimal Precio { get; set; }
        [Column("INCLUY_IVA")]
        public bool IncluyeIva { get; set; }
        [Column("DescripcionPedido")]
        public string DescripcionDelPedido { get; set; }
        [Column("DescripcionAdicionalPedido")]
        public string DescripcionDelPedidoAdicional { get; set; }
        [Column("Ancho_ME")]
        public int MedidaEspecial_Ancho { get; set; }
        [Column("Largo_ME")]
        public int MedidaEspecial_Largo { get; set; }
        [Column("Espesor_ME")]
        public int MedidaEspecial_Espesor { get; set; }
    }
}
