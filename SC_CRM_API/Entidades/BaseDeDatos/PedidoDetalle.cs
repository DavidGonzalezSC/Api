using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("VW_PedidoDetalle")]
    public class PedidoDetalle
    {
        public string Tipo { get; set; }
        public int Talonario { get; set; }
        public int Numero { get; set; }        
        public string NRO_PEDIDO { get; set; }        
        public string FechaPedido { get; set; }
        public int IdSucursal { get; set; }
        public string CodTransporte { get; set; }
        public string CodigoCliente { get; set; }
        public string RazonSocial { get; set; }
        public string TelefonoEntrega1 { get; set; }
        public string TelefonoEntrega2 { get; set; }
        public string MailCliente { get; set; }
        public string FechaEntrega { get; set; }
        public string CalleEntrega { get; set; }
        public string NumeroCalleEntrega { get; set; }
        public string PisoEntrega { get; set; }
        public string DepartamentoEntrega { get; set; }
        public string EntreCalleEntrega1 { get; set; }
        public string EntreCalleEntrega2 { get; set; }
        public string LocalidadEntrega { get; set; }
        public string CodigoPostalEntrega { get; set; }
        public string ProvinciaEntrega { get; set; }
        public string PaisEntrega { get; set; }
        public double LatitudEntrega { get; set; }
        public double LongitudEntrega { get; set; }
        public string RangoHorarioEntrega { get; set; }
        public int HoraDesdeEntrega { get; set; }
        public int HoraHastaEntrega { get; set; }
        public string ObservacionesEntrega { get; set; }
        public string NombreCF { get; set; }
        public string CUIT { get; set; }
        public string TelefonoCF1 { get; set; }
        public string TelefonoCF2 { get; set; }
        public string EmailCF { get; set; }
        public string FechaEntregaCF { get; set; }
        public string CalleEntregaCF { get; set; }
        public string NumeroCalleCF { get; set; }
        public string PisoEntregaCF { get; set; }
        public string DepartamentoEntregaCF { get; set; }
        public string EntreCalleCF1 { get; set; }
        public string EntreCalleCF2 { get; set; }
        public string LocalidadEntregaCF { get; set; }
        public string CodigoPostalEntregaCF { get; set; }
        public string ProvinciaEntregaCF { get; set; }
        public string PaisEntregaCF { get; set; }
        public string RangoHorarioEntregaCF { get; set; }
        public int HoraDesdeEntregaCF { get; set; }
        public int HoraHastaEntregaCF { get; set; }
        public int CantidadDetalle { get; set; }
        public int RenglonOrdenCompra { get; set; }
        public decimal Ancho { get; set; }
        public decimal Largo { get; set; }
        public decimal Espesor { get; set; }
        public string DescripcionCliente { get; set; }
        public double Cantidad { get; set; }
        public string DetalleAdicional { get; set; }
        public decimal ValorDeclarado { get; set; }
        public Int16 Talon_ped { get; set; }
        public int ID_GVA03 { get; set; }
        public int N_RENGLON { get; set; }
        public string CodArticulo { get; set; }
        public int RenglonPedido { get; set; }
        public string Deposito { get; set; }
        public string COD_CLIENT { get; set; }
        public DateTime FECHA_PEDI { get; set; }
        public string Bonificacion { get; set; }

    }
}
