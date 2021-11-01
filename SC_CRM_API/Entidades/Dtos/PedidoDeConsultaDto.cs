using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.Dtos
{
    public class PedidoDeConsultaDto
    {
        public PedidoDtoCabecera Cabecera { get; set; } = new PedidoDtoCabecera();
        public List<PedidoDtoDetalle> Detalles { get; set; } = new List<PedidoDtoDetalle>();
    }

    public class PedidoDtoCabecera
    {
        
        public Int16? TalonPedido { get; set; }
        public string Nro_Pedido { get; set; }
        public string Cod_Cliente { get; set; }
        public string RazonSocial { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string Domicilio { get; set; }
        public string CodPostal { get; set; }
        public string Localidad { get; set; }
        public string Vendedor { get; set; }
        public string Lista { get; set; }
        public string CodSucursal { get; set; }
        public string Email { get; set; }
        public DateTime? FechaPedido { get; set; }
        public string Clasificacion { get; set; }
        public string Cuit { get; set; }
        public DateTime? FechaDeEntrega { get; set; }
        public string LeyendaDeEntrega { get; set; }
        public string Comentarios { get; set; }
        public string Transporte { get; set; }
        public string Leyenda_1 { get; set; }
        public string Leyenda_2 { get; set; }
        public string RangoHorario { get; set; }
        public string Entrega_Calle { get; set; }
        public string Entrega_Numero { get; set; }
        public string Entrega_Piso { get; set; }
        public string Entrega_Depto { get; set; }
        public string Entrega_CP { get; set; }
        public string Entrega_Localidad { get; set; }
        public string Entrega_Provincia { get; set; }
        public string Entrega_Pais { get; set; }

    }

    public class PedidoDtoDetalle
    {
        public int Renglon { get; set; }
        public string Cod_articulo { get; set; }
        public string Descripcion { get; set; }
        public string Descripcion_Adicional { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Descuento { get; set; }
        public decimal? Precio { get; set; }
        public bool IncluyeIva { get; set; }
        public string DescripcionDelPedido { get; set; }
        public string DescripcionDelPedidoAdicional { get; set; }
        public int? MedidaEspecial_Ancho { get; set; }
        public int? MedidaEspecial_Largo { get; set; }
        public int? MedidaEspecial_Espesor { get; set; }
    }
}
