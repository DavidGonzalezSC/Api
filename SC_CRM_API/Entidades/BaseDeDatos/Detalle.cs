using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    [Table("CRM_PresupuestosDetalles_TEMP")]
    public class DetalleTemp
    {
        public DetalleTemp()
        {

        }

        public DetalleTemp(string sucursal, Guid idEvento)
        {
            Sucursal = sucursal;
            IdEvento = idEvento;
        }

        [Key]
        [Required]
        [Column("ID_PresupuestosDetalles_Temporal")]
        public int IdTemp { get; set; }

        public string Sucursal { get; set; }

        [Column("ID_Evento")]
        public Guid IdEvento { get; set; }

        public int Creacion { get; set; }

        [Column("PasarAPedido")]
        public bool RenglonAPedido { get; set; }

    }

    public class ValidarDetalle : AbstractValidator<Detalle>
    {
        public ValidarDetalle()
        {
            /*
            RuleFor(c => c.IdPresupuestoDetalle).NotNull();
            RuleFor(c => c.IdPresupuesto).NotNull();
            RuleFor(c => c.Version).NotNull();
            RuleFor(c => c.CodigoArticulo).NotNull().MaximumLength(15);
            RuleFor(c => c.Descripcion).NotNull().MaximumLength(7900);
            RuleFor(c => c.Cantidad).NotNull().ScalePrecision(5, 18);
            RuleFor(c => c.CantidadPendiente).NotNull().ScalePrecision(5, 18);
            RuleFor(c => c.Precio).NotNull().ScalePrecision(5, 18);
            RuleFor(c => c.Moneda).NotNull();
            RuleFor(c => c.Bonif).NotNull().ScalePrecision(2, 5);
            RuleFor(c => c.Bonif_2).NotNull().ScalePrecision(2, 5);
            RuleFor(c => c.Bonif_3).NotNull().ScalePrecision(2, 5);
            RuleFor(c => c.Um).NotNull().MaximumLength(1);
            RuleFor(c => c.IdOrden).NotNull();
            RuleFor(c => c.Costo).NotNull().ScalePrecision(5, 18);
            RuleFor(c => c.Grupo).NotNull().MaximumLength(15);
            RuleFor(c => c.Opcional).NotNull();
            RuleFor(c => c.Obligatorio).NotNull();
            RuleFor(c => c.Ocultar).NotNull();
            RuleFor(c => c.Adicional).NotNull();
            RuleFor(c => c.NumeroDellave).NotNull().MaximumLength(10);
            RuleFor(c => c.FacturaAx).MaximumLength(200);
            RuleFor(c => c.PorcentajeAx).ScalePrecision(2, 18);
            RuleFor(c => c.PromocionAx).ScalePrecision(2, 18);
            RuleFor(c => c.ObservacionesAx).MaximumLength(200);
            RuleFor(c => c.PorcentajeDos).NotNull().ScalePrecision(2, 18);
            RuleFor(c => c.AprobadoAx).NotNull();
            RuleFor(c => c.Cantidad_2).ScalePrecision(5, 18);
            RuleFor(c => c.Cantidad_3).ScalePrecision(5, 18);
            RuleFor(c => c.Campo_1).NotNull().MaximumLength(2000);
            RuleFor(c => c.Campo_2).NotNull().MaximumLength(2000);
            RuleFor(c => c.Campo_3).NotNull().MaximumLength(2000);
            RuleFor(c => c.Cantidad_4).ScalePrecision(5, 18);
            RuleFor(c => c.Cantidad_5).ScalePrecision(5, 18);
            */

        }
	}

    public class Detalle : DetalleTemp
    {
        public Detalle()
        {

        }

        public Detalle(string sucursal, Guid idEvento) : base(sucursal, idEvento)
        {
        }

        [Column("ID_PresupuestoDetalle")]                 //[int] IDENTITY(1,1) NOT NULL,
        public int IdPresupuestoDetalle { get; set; }

        [Column("ID_Presupuesto")]                      // [int] NOT NULL,
        public int IdPresupuesto { get; set; }

        [Column("Version")]                             // [int] NOT NULL,
        public int Version { get; set; }

        [Column("Cod_Articu")]                          // [varchar] (15) NOT NULL,
        public string CodigoArticulo { get; set; }

        [Column("Descripcion")]                         //[varchar] (7900) NOT NULL,
        public string Descripcion { get; set; }

        [Column("Cantidad")]                                //[decimal](18, 5) NOT NULL,
        public decimal Cantidad { get; set; }

        [Column("CantidadPendiente")]                       //[decimal](18, 5) NOT NULL,
        public decimal CantidadPendiente { get; set; }

        [Column("Precio")]                              //[decimal](18, 5) NOT NULL,
        public decimal Precio { get; set; }

        [Column("Moneda")]                              //[int] NOT NULL,
        public int Moneda { get; set; }

        [Column("Bonif")]                                   //[decimal](5, 2) NOT NULL,
        public decimal Bonif { get; set; }

        [Column("Bonif2")]                              //[decimal](5, 2) NOT NULL,
		public decimal Bonif_2 { get; set; }

		[Column("Bonif3")]                              //[decimal](5, 2) NOT NULL,
		public decimal Bonif_3 { get; set; }

		[Column("UM")]                                  //[varchar] (1) NOT NULL,
        public string Um { get; set; }

        [Column("ID_Orden")]                                //[int] NOT NULL,
        public int IdOrden { get; set; }

        [Column("Costo")]                                   //[decimal](18, 5) NOT NULL,
        public decimal Costo { get; set; }

        [Column("Grupo")]                                   //[varchar] (15) NOT NULL,
        public string Grupo { get; set; }

        [Column("Opcional")]                                //[bit] NOT NULL,
        public bool Opcional { get; set; }

        [Column("Obligatorio")]                         //[bit] NOT NULL,
        public bool Obligatorio { get; set; }

        [Column("Ocultar")]                             //[bit]	NOT NULL,
        public bool Ocultar { get; set; }

        [Column("Adicional")]                               //[bit] NOT NULL,
        public bool Adicional { get; set; }

        [Column("Nro_llave")]                               //[varchar] (10) NULL,
        public string NumeroDellave { get; set; }

        [Column("FacturaAX")]                               //[varchar] (200) NULL,
        public string FacturaAx { get; set; }

        [Column("Informado")]                               //[datetime] NULL,
        public DateTime? Informado { get; set; }

        [Column("PorcentajeAX")]                            //[decimal](18, 2) NULL,
        public decimal? PorcentajeAx { get; set; }

        [Column("promocionAX")]                         //[decimal](18, 2) NULL,
        public decimal? PromocionAx { get; set; }

        [Column("observacionesAX")]                     //[varchar] (200) NOT NULL,
        public string ObservacionesAx { get; set; }

        [Column("Porcentaje2")]                         //[decimal](18, 2) NOT NULL,
        public decimal PorcentajeDos { get; set; }

        [Column("FOTO")]                                    //[image] NULL,
        public byte[] Foto { get; set; }

        [Column("AprobadoAX")]                          //[bit] NOT NULL,
        public bool AprobadoAx { get; set; }

        [Column("Cantidad2")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_2 { get; set; }

        [Column("Cantidad3")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_3 { get; set; }

        [Column("Campo1")]								//[varchar] (2000) NOT NULL,
        public string Campo_1 { get; set; }

        [Column("Campo2")]								//[varchar] (2000) NOT NULL,
        public string Campo_2 { get; set; }

        [Column("Campo3")]								//[varchar] (2000) NOT NULL, //se va a guardar el codigo de transporte
        public string Campo_3 { get; set; }

        [Column("Campo4")]								//[datetime] NULL,
        public DateTime? Campo_4 { get; set; }

        [Column("Campo5")]								//[datetime] NULL,
        public DateTime? Campo_5 { get; set; }

        [Column("FechaAltaRenglon")]						//[datetime] NULL,
        public DateTime? FechaDeAltaDelRenglon { get; set; }

        [Column("Cantidad4")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_4 { get; set; }

        [Column("Cantidad5")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_5 { get; set; }

        //-- Agregado para los Domicilios por Renglon
        [Column("CA_ListaPrecios")]
        public int? CodigoListaPrecio { get; set; }           //int null

        [Column("CA_Descuento")]
        public string CodigoDescuento { get; set; }            // varchar(6)

        [Column("CA_FechaEntrega")]
        public DateTime? FechaEntrega { get; set; }      //datetime, null

        public int? CA_IdDireccionEntrega { get; set; }    //int null

        [Column("CA_AnchoME")]
        public int? Ancho_ME { get; set; }

        [Column("CA_LargoME")]
        public int? Largo_ME { get; set; }

        [Column("CA_EspesorME")]
        public int? Espesor_ME { get; set; }

        [Column("CA_ComentarioAdicional")]
        public string ComentarioAdicional { get; set; }

        [NotMapped]
        public string NombreDomicilio { get; set; } //utilizado para pasar la definicion desde el renglon de domicilio y matchear cuando se escriben con el Id retiornado por el SP de sucursales
        
        //[NotMapped]
        //public IEnumerable<string> CodigosValidados = new List<string>(); //utilizado para alojar los SP que se quieran puentear

    }

    [Table("VW_CRM_PRESUPUESTOSDETALLES")]
    public class DetallesConVista : DetalleDeConsulta
    {
        [Column("ID_PresupuestoDetalle")]
        public int IdPresupuestoDetalle { get; set; }

        public string Editable { get; set; }
    }

    [Table("CRM_PresupuestosDetalles")]
    public class DetallesEnTabla : DetalleDeConsulta
    {
        [Column("ID_PresupuestoDetalle")]
        [Key]                 //[int] IDENTITY(1,1) NOT NULL,
        public int IdPresupuestoDetalle { get; set; }
    }

    //--Directo para consultas
    
    public class DetalleDeConsulta
    {

        [Column("ID_Presupuesto")]                      // [int] NOT NULL,
        public int IdPresupuesto { get; set; }

        [Column("Version")]                             // [int] NOT NULL,
        public int Version { get; set; }

        [Column("Cod_Articu")]                          // [varchar] (15) NOT NULL,
        public string CodigoArticulo { get; set; }

        [Column("Descripcion")]                         //[varchar] (7900) NOT NULL,
        public string Descripcion { get; set; }

        [Column("Cantidad")]                                //[decimal](18, 5) NOT NULL,
        public decimal Cantidad { get; set; }

        [Column("CantidadPendiente")]                       //[decimal](18, 5) NOT NULL,
        public decimal CantidadPendiente { get; set; }

        [Column("Precio")]                              //[decimal](18, 5) NOT NULL,
        public decimal Precio { get; set; }

        [Column("Moneda")]                              //[int] NOT NULL,
        public int Moneda { get; set; }

        [Column("Bonif")]                                   //[decimal](5, 2) NOT NULL,
        public decimal Bonif { get; set; }

        [Column("Bonif2")]                              //[decimal](5, 2) NOT NULL,
        public decimal Bonif_2 { get; set; }

        [Column("Bonif3")]                              //[decimal](5, 2) NOT NULL,
        public decimal Bonif_3 { get; set; }

        [Column("UM")]                                  //[varchar] (1) NOT NULL,
        public string Um { get; set; }

        [Column("ID_Orden")]                                //[int] NOT NULL,
        public int IdOrden { get; set; }

        [Column("Costo")]                                   //[decimal](18, 5) NOT NULL,
        public decimal Costo { get; set; }

        [Column("Grupo")]                                   //[varchar] (15) NOT NULL,
        public string Grupo { get; set; }

        [Column("Opcional")]                                //[bit] NOT NULL,
        public bool Opcional { get; set; }

        [Column("Obligatorio")]                         //[bit] NOT NULL,
        public bool Obligatorio { get; set; }

        [Column("Ocultar")]                             //[bit]	NOT NULL,
        public bool Ocultar { get; set; }

        [Column("Adicional")]                               //[bit] NOT NULL,
        public bool Adicional { get; set; }

        [Column("Nro_llave")]                               //[varchar] (10) NULL,
        public string NumeroDellave { get; set; }

        [Column("FacturaAX")]                               //[varchar] (200) NULL,
        public string FacturaAx { get; set; }

        [Column("Informado")]                               //[datetime] NULL,
        public DateTime? Informado { get; set; }

        [Column("PorcentajeAX")]                            //[decimal](18, 2) NULL,
        public decimal? PorcentajeAx { get; set; }

        [Column("promocionAX")]                         //[decimal](18, 2) NULL,
        public decimal? PromocionAx { get; set; }

        [Column("observacionesAX")]                     //[varchar] (200) NOT NULL,
        public string ObservacionesAx { get; set; }

        [Column("Porcentaje2")]                         //[decimal](18, 2) NOT NULL,
        public decimal PorcentajeDos { get; set; }

        [Column("FOTO")]                                    //[image] NULL,
        public byte[] Foto { get; set; }

        [Column("AprobadoAX")]                          //[bit] NOT NULL,
        public bool AprobadoAx { get; set; }

        [Column("Cantidad2")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_2 { get; set; }

        [Column("Cantidad3")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_3 { get; set; }

        [Column("Campo1")]								//[varchar] (2000) NOT NULL,
        public string Campo_1 { get; set; }

        [Column("Campo2")]								//[varchar] (2000) NOT NULL,
        public string Campo_2 { get; set; }

        [Column("Campo3")]								//[varchar] (2000) NOT NULL,
        public string Campo_3 { get; set; }

        [Column("Campo4")]								//[datetime] NULL,
        public DateTime? Campo_4 { get; set; }

        [Column("Campo5")]								//[datetime] NULL,
        public DateTime? Campo_5 { get; set; }

        [Column("FechaAltaRenglon")]						//[datetime] NULL,
        public DateTime? FechaDeAltaDelRenglon { get; set; }

        [Column("Cantidad4")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_4 { get; set; }

        [Column("Cantidad5")]								//[decimal](18, 5) NOT NULL,
        public decimal Cantidad_5 { get; set; }

        //-- Agregado para los Domicilios por Renglon
        [Column("CA_ListaPrecios")]
        public int? CodigoListaPrecio { get; set; }           //int null

        [Column("CA_Descuento")]
        public string CodigoDescuento { get; set; }            // varchar(6)

        [Column("CA_FechaEntrega")]
        public DateTime? FechaEntrega { get; set; }      //datetime, null

        [Column("CA_IdDireccionEntrega")]
        public int? CA_IdDireccionEntrega { get; set; }    //int null

        [Column("CA_AnchoME")]
        public int? Ancho_ME { get; set; }

        [Column("CA_LargoME")]
        public int? Largo_ME { get; set; }

        [Column("CA_EspesorME")]
        public int? Espesor_ME { get; set; }

        [Column("CA_ComentarioAdicional")]
        public string ComentarioAdicional { get; set; }
    }
}

