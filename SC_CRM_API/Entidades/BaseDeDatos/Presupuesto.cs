using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{

    [Table("CRM_PRESUPUESTOS_TEMP")]
    public class PresupuestoTemporal
    {
        public PresupuestoTemporal()
        {

        }

        public PresupuestoTemporal(string sucursal, Guid idEvento)
        {
            Sucursal = sucursal;
            IdEvento = idEvento;
        }

        [Key]
        [Required]
        [Column("ID_Presupuesto_Temporal")]
        public int IdTemp { get; set; }

        public string Sucursal { get; set; }

        [Column("ID_Evento")]
        public Guid IdEvento { get; set; }

        public int Creacion { get; set; }

    }

    public class ValidarPresupuesto : AbstractValidator<Presupuesto>
    {
        public ValidarPresupuesto()
        {
            /*
            RuleFor(c => c.IdPresupuesto).NotNull().Equal(0);
            RuleFor(c => c.Version).NotNull();
            RuleFor(c => c.IdCliente).NotNull();
            RuleFor(c => c.IdCategoria).NotNull();
            RuleFor(c => c.FechaDeAlta).NotNull();
            
            
            RuleFor(c => c.CondicionDeVenta).NotNull();
            RuleFor(c => c.CodigoDeTransporte).NotNull();
            RuleFor(c => c.NumeroDeLista).NotNull();
            RuleFor(c => c.CodigoDeDeposito).NotNull().NotEmpty().MaximumLength(2);
            RuleFor(c => c.Bonificacion).NotNull().ScalePrecision(2, 4);
            RuleFor(c => c.IdVendedor).NotNull();
            RuleFor(c => c.VendedorAsignado).NotNull();
            RuleFor(c => c.MonCte).NotNull();
            RuleFor(c => c.Cotizacion).NotNull().ScalePrecision(5, 18);
            RuleFor(c => c.ProximoContacto);
            RuleFor(c => c.Finalizada);
            RuleFor(c => c.Observaciones).NotNull().MaximumLength(8000);
            RuleFor(c => c.IdEstadoDelPresupuesto).NotNull().MaximumLength(3);
            RuleFor(c => c.IdEstadoDelPresupuestoAnterior).NotNull().MaximumLength(3);
            RuleFor(c => c.FechaDeUltimoCambio).NotNull();
            RuleFor(c => c.IdVendedorDelUltimoCambio).NotNull();
            RuleFor(c => c.IdMotivoEstado).NotNull().MaximumLength(3);
            RuleFor(c => c.Comentarios).NotNull().MaximumLength(8000);
            RuleFor(c => c.Prioridad).NotNull();
            RuleFor(c => c.TalonarioDePedidos).NotNull();
            RuleFor(c => c.FechaDeCotizacion).NotNull();
            RuleFor(c => c.IdProyecto).NotNull();
            RuleFor(c => c.CodigoDeClasificacion).NotNull().MaximumLength(6);
            RuleFor(c => c.IdDeContacto).NotNull();
            RuleFor(c => c.IdDeReferencia).NotNull();
            RuleFor(c => c.NumeroDEComprobante).NotNull().MaximumLength(6);
            RuleFor(c => c.PorcentajeDos).NotNull().ScalePrecision(2, 18);
            RuleFor(c => c.TalonarioDePedidos).NotNull();
            RuleFor(c => c.IdDeAcopio).NotNull();
            RuleFor(c => c.IdDeSucursal).NotNull();
            RuleFor(c => c.Privado).NotNull().MaximumLength(1);
            RuleFor(c => c.Campo_1).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_2).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_3).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_4).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_5).NotNull().MaximumLength(1000);
            RuleFor(c => c.Perfil).NotNull().MaximumLength(6);
            RuleFor(c => c.IdSubCategoria).NotNull();
            RuleFor(c => c.CodigoAsientoModeloGv).NotNull().MaximumLength(30);
            RuleFor(c => c.Fecha).NotNull();
            RuleFor(c => c.MailEnviado).NotNull().MaximumLength(2);
            RuleFor(c => c.DocumentacionRecibida).NotNull().MaximumLength(2);
            RuleFor(c => c.Documentacion).NotNull().MaximumLength(20);
            RuleFor(c => c.EstadoMagento).NotNull().MaximumLength(15);
            RuleFor(c => c.EstadoVeraz).NotNull().MaximumLength(20);
            RuleFor(c => c.ScoreVeraz).NotNull().MaximumLength(10);
            RuleFor(c => c.Banco).NotNull().MaximumLength(25);
            RuleFor(c => c.Tarjeta).NotNull().MaximumLength(100);
            RuleFor(c => c.Cuotas).NotNull().MaximumLength(10);
            RuleFor(c => c.NumeroDeLote).NotNull().MaximumLength(10);
            RuleFor(c => c.NumeroDeCupon).NotNull().MaximumLength(10);
            RuleFor(c => c.CodigoDeAutorizacion).NotNull().MaximumLength(15);
            RuleFor(c => c.NumeroDeTarjeta).NotNull().MaximumLength(20);
            RuleFor(c => c.OrdenMagento).NotNull().MaximumLength(15);
            RuleFor(c => c.NpsTransactionID).NotNull().MaximumLength(20);
            RuleFor(c => c.CodigoPresupuestoNPS).NotNull().MaximumLength(3);
            RuleFor(c => c.ImporteOriginal).NotNull().ScalePrecision(2, 18);
            RuleFor(c => c.NpsMsgRespuesta).NotNull().MaximumLength(255);
            */
          

        }
    }
    

    public class Presupuesto : PresupuestoTemporal
	{

        public Presupuesto()
        {
        }
        
        public Presupuesto(string sucursal, Guid idEvento) : base(sucursal, idEvento)
        {
        }

        [Column("ID_Presupuesto")]                                                              //[int] NOT NULL,
        public int IdPresupuesto { get; set; }

        [Column("Version")]                                                                     //[int] NOT NULL,
        public int Version { get; set; }

        [Column("ID_Cliente")]                                                                  //[int] NOT NULL,
        [Required]
        public int IdCliente { get; set; }

        [Column("ID_Categoria")]                                                                //[int] NOT NULL,
        [Required]
        public int IdCategoria { get; set; }

        [Column("Fecha_Alta")]                                                                  //[datetime] NOT NULL,
        [Required]
        public DateTime FechaDeAlta { get; set; }

        [Column("Fecha_Entrega")]                                                               //[datetime] NULL,
        public DateTime? FechaDeEntrega { get; set; }

        [Column("FechaVigencia")]                                                               //[datetime] NULL,
        public DateTime? FechaDeVigencia { get; set; }

        [Column("Cond_Vta")]                                                                    //[int] NOT NULL,
        public int CondicionDeVenta { get; set; }

        [Column("COD_TRANSP")]                                                                  //[varchar](10) NOT NULL,
        public string CodigoDeTransporte { get; set; }

        [Column("Nro_Lista")]                                                                   //[int] NOT NULL,
        public int NumeroDeLista { get; set; }

        [Column("Cod_Deposito")]                                                                //[varchar](2) NOT NULL,
        public string CodigoDeDeposito { get; set; }

        
        [Column("Bonif")]                                                                       //[decimal](4, 2) NOT NULL,
        public decimal Bonificacion { get; set; }

        [Column("ID_Vendedor")]                                                                 //[int] NOT NULL,
        public int IdVendedor { get; set; }

        [Column("ID_VendedorAsignado")]                                                         //[int] NOT NULL,
        public int VendedorAsignado { get; set; }

        [Column("Mon_Cte")]                                                                     //[bit] NOT NULL,
        public bool MonCte { get; set; }

        [Column("Cotizacion")]                                                                  //[decimal](18, 5) NOT NULL,
        public decimal Cotizacion { get; set; }

        [Column("Prox_Contacto")]                                                               //[datetime] NULL,
        public DateTime? ProximoContacto { get; set; }

        [Column("Finalizada")]                                                                  //[datetime] NULL,
        public DateTime? Finalizada { get; set; }

        [Column("Observaciones")]                                                               //[varchar](8000) NOT NULL,
        public string Observaciones { get; set; }

        [Column("ID_EstadoPresupuesto")]                                                        //[varchar](3) NOT NULL,
        public string IdEstadoDelPresupuesto { get; set; }

        [Column("ID_EstadoPresupuestoAnterior")]                                                //[varchar](3) NULL,
        public string IdEstadoDelPresupuestoAnterior { get; set; }

        [Column("FechaUltimoCambio")]                                                           //[datetime] NOT NULL,
        public DateTime FechaDeUltimoCambio { get; set; }

        [Column("ID_VendedUltimoCambio")]														//[int] NOT NULL,
        public int IdVendedorDelUltimoCambio { get; set; }

        [Column("ID_MotivoEstado")]																//[varchar](3) NULL,
        public string IdMotivoEstado { get; set; }

        [Column("Comentarios")]																	//[varchar](8000) NOT NULL,
        public string Comentarios { get; set; }

        [Column("Prioridad")]																	//[int] NOT NULL,
        public int Prioridad { get; set; }

        [Column("Talonario_pedidos")]															//[int] NULL,
        public int TalonarioDePedidos { get; set; }

        [Column("FechaCotizacion")]																//[datetime] NOT NULL,
        public DateTime FechaDeCotizacion { get; set; }

        [Column("ID_Proyecto")]																	//[int] NULL,
        public int IdProyecto { get; set; }

        [Column("COD_CLASIF")]																	//[varchar](6) NOT NULL,
        public string CodigoDeClasificacion { get; set; }

        [Column("ID_Contacto")]																	//[int] NULL,
        public int IdDeContacto { get; set; }

        [Column("ID_Referencia")]																//[int] NULL,
        public int IdDeReferencia { get; set; }

        [Column("NRO_O_COMP")]																	//[varchar](14) NOT NULL,
        public string NumeroDEComprobante { get; set; }

        [Column("Porcentaje2")]                         		    							//[decimal](18, 2) NOT NULL,
        public decimal PorcentajeDos { get; set; }

        [Column("Talonario_pedidos_2")]															//[int] NULL,
        public int TalonarioDePedidosDos { get; set; }

        [Column("ID_Acopio")]																	//[int] NULL,
        public int IdDeAcopio { get; set; }

        [Column("ID_Sucursal")]																	//[int] NULL,
        public int IdDeSucursal { get; set; }

        [Column("Privado")]																		//[varchar](1) NOT NULL,
        public string Privado { get; set; }

        [Column("Campo1")]																		//[varchar](1000) NOT NULL,
        public string Campo_1 { get; set; }

        [Column("Campo2")]																		//[varchar](1000) NOT NULL,
        public string Campo_2 { get; set; }

        [Column("Campo3")]																		//[varchar](1000) NOT NULL,
        public string Campo_3 { get; set; }

        [Column("Campo4")]																		//[varchar](1000) NOT NULL,
        public string Campo_4 { get; set; }

        [Column("Campo5")]																		//[varchar](1000) NOT NULL,
        public string Campo_5 { get; set; }

        [Column("perfil")]																		//[varchar](6) NOT NULL,
        public string Perfil { get; set; }

        [Column("ID_subCategoria")]																//[int] NULL,
        public int IdSubCategoria { get; set; }

        [Column("COD_ASIENTO_MODELO_GV")]														//[varchar](30) NOT NULL,
        public string CodigoAsientoModeloGv { get; set; }

        [Column("Fecha")]																		//[datetime] NOT NULL,
        public DateTime Fecha { get; set; }

        [Column("CA_MailEnviado")]																//[varchar](2) NULL,
        public string MailEnviado { get; set; }

        [Column("CA_DocRecibida")]																//[varchar](2) NULL,
        public string DocumentacionRecibida { get; set; }

        [Column("CA_Documentacion")]															//[varchar](20) NULL,
        public string Documentacion { get; set; }

        [Column("CA_EstadoMagento")]															//[varchar](15) NULL,
        public string EstadoMagento { get; set; }

        [Column("CA_EstadoVeraz")]																//[varchar](20) NULL,
        public string EstadoVeraz { get; set; }

        [Column("CA_ScoreVeraz")]																//[varchar](10) NULL,
        public string ScoreVeraz { get; set; }

        [Column("CA_Banco")]																	//[varchar](25) NULL,
        public string Banco { get; set; }

        [Column("CA_Tarjeta")]																	//[varchar](100) NULL,
        public string Tarjeta { get; set; }

        [Column("CA_Cuotas")]																	//[varchar](10) NULL,
        public string Cuotas { get; set; }

        [Column("CA_NroLote")]																	//[varchar](10) NULL,
        public string NumeroDeLote { get; set; }

        [Column("CA_NroCupon")]																	//[varchar](10) NULL,
        public string NumeroDeCupon { get; set; }

        [Column("CA_CodAutorizacion")]															//[varchar](15) NULL,
        public string CodigoDeAutorizacion { get; set; }

        [Column("CA_NroTarjeta")]																//[varchar](20) NULL,
        public string NumeroDeTarjeta { get; set; }

        [Column("CA_OrdenMagento")]																//[varchar](15) NULL,
        public string OrdenMagento { get; set; }

        [Column("CA_NPSTransactionID")]															//[varchar](20) NULL,
        public string NpsTransactionID { get; set; }

        [Column("CA_CodRespuestaNPS")]															//[varchar](3) NULL,
        public string CodigoPresupuestoNPS { get; set; }

        [Column("CA_ImporteOriginal", TypeName = "decimal(18,2)")]								//[decimal](18, 2) NULL,
        public decimal ImporteOriginal { get; set; }

        [Column("CA_NPSMsgRespuesta")]															//[varchar](255) NULL,
        public string NpsMsgRespuesta { get; set; }


    }

    [Table("CRM_PRESUPUESTOS")]
    public class PresupuestoDeConsulta
    {
        [Key]
        [Column("ID_Presupuesto")]                                                              //[int] NOT NULL,
        public int IdPresupuesto { get; set; }

        [Column("Version")]                                                                     //[int] NOT NULL,
        public int Version { get; set; }

        [Column("ID_Cliente")]                                                                  //[int] NOT NULL,
        [Required]
        public int IdCliente { get; set; }

        [Column("ID_Categoria")]                                                                //[int] NOT NULL,
        [Required]
        public int IdCategoria { get; set; }

        [Column("Fecha_Alta")]                                                                  //[datetime] NOT NULL,
        [Required]
        public DateTime FechaDeAlta { get; set; }

        [Column("Fecha_Entrega")]                                                               //[datetime] NULL,
        public DateTime? FechaDeEntrega { get; set; }

        [Column("FechaVigencia")]                                                               //[datetime] NULL,
        public DateTime? FechaDeVigencia { get; set; }

        [Column("Cond_Vta")]                                                                    //[int] NOT NULL,
        public int CondicionDeVenta { get; set; }

        [Column("COD_TRANSP")]                                                                  //[varchar](10) NOT NULL,
        public string CodigoDeTransporte { get; set; }

        [Column("Nro_Lista")]                                                                   //[int] NOT NULL,
        public int NumeroDeLista { get; set; }

        [Column("Cod_Deposito")]                                                                //[varchar](2) NOT NULL,
        public string CodigoDeDeposito { get; set; }


        [Column("Bonif")]                                                                       //[decimal](4, 2) NOT NULL,
        public decimal Bonificacion { get; set; }

        [Column("ID_Vendedor")]                                                                 //[int] NOT NULL,
        public int IdVendedor { get; set; }

        [Column("ID_VendedorAsignado")]                                                         //[int] NOT NULL,
        public int VendedorAsignado { get; set; }

        [Column("Mon_Cte")]                                                                     //[bit] NOT NULL,
        public bool MonCte { get; set; }

        [Column("Cotizacion")]                                                                  //[decimal](18, 5) NOT NULL,
        public decimal Cotizacion { get; set; }

        [Column("Prox_Contacto")]                                                               //[datetime] NULL,
        public DateTime? ProximoContacto { get; set; }

        [Column("Finalizada")]                                                                  //[datetime] NULL,
        public DateTime? Finalizada { get; set; }

        [Column("Observaciones")]                                                               //[varchar](8000) NOT NULL,
        public string Observaciones { get; set; }

        [Column("ID_EstadoPresupuesto")]                                                        //[varchar](3) NOT NULL,
        public string IdEstadoDelPresupuesto { get; set; }

        [Column("ID_EstadoPresupuestoAnterior")]                                                //[varchar](3) NULL,
        public string IdEstadoDelPresupuestoAnterior { get; set; }

        [Column("FechaUltimoCambio")]                                                           //[datetime] NOT NULL,
        public DateTime FechaDeUltimoCambio { get; set; }

        [Column("ID_VendedUltimoCambio")]														//[int] NOT NULL,
        public int IdVendedorDelUltimoCambio { get; set; }

        [Column("ID_MotivoEstado")]																//[varchar](3) NULL,
        public string IdMotivoEstado { get; set; }

        [Column("Comentarios")]																	//[varchar](8000) NOT NULL,
        public string Comentarios { get; set; }

        [Column("Prioridad")]																	//[int] NOT NULL,
        public int Prioridad { get; set; }

        [Column("Talonario_pedidos")]															//[int] NULL,
        public int TalonarioDePedidos { get; set; }

        [Column("FechaCotizacion")]																//[datetime] NOT NULL,
        public DateTime FechaDeCotizacion { get; set; }

        [Column("ID_Proyecto")]																	//[int] NULL,
        public int? IdProyecto { get; set; }

        [Column("COD_CLASIF")]																	//[varchar](6) NOT NULL,
        public string CodigoDeClasificacion { get; set; }

        [Column("ID_Contacto")]																	//[int] NULL,
        public int? IdDeContacto { get; set; }

        [Column("ID_Referencia")]																//[int] NULL,
        public int? IdDeReferencia { get; set; }

        [Column("NRO_O_COMP")]																	//[varchar](14) NOT NULL,
        public string NumeroDEComprobante { get; set; }

        [Column("Porcentaje2")]                         		    							//[decimal](18, 2) NOT NULL,
        public decimal PorcentajeDos { get; set; }

        [Column("Talonario_pedidos_2")]															//[int] NULL,
        public int TalonarioDePedidosDos { get; set; }

        [Column("ID_Acopio")]																	//[int] NULL,
        public int? IdDeAcopio { get; set; }

        [Column("ID_Sucursal")]																	//[int] NULL,
        public int? IdDeSucursal { get; set; }

        [Column("Privado")]																		//[varchar](1) NOT NULL,
        public string Privado { get; set; }

        [Column("Campo1")]																		//[varchar](1000) NOT NULL,
        public string Campo_1 { get; set; }

        [Column("Campo2")]																		//[varchar](1000) NOT NULL,
        public string Campo_2 { get; set; }

        [Column("Campo3")]																		//[varchar](1000) NOT NULL,
        public string Campo_3 { get; set; }

        [Column("Campo4")]																		//[varchar](1000) NOT NULL,
        public string Campo_4 { get; set; }

        [Column("Campo5")]																		//[varchar](1000) NOT NULL,
        public string Campo_5 { get; set; }

        [Column("perfil")]																		//[varchar](6) NOT NULL,
        public string Perfil { get; set; }

        [Column("ID_subCategoria")]																//[int] NULL,
        public int? IdSubCategoria { get; set; }

        [Column("COD_ASIENTO_MODELO_GV")]														//[varchar](30) NOT NULL,
        public string CodigoAsientoModeloGv { get; set; }

        [Column("Fecha")]																		//[datetime] NOT NULL,
        public DateTime Fecha { get; set; }

        [Column("CA_MailEnviado")]																//[varchar](2) NULL,
        public string MailEnviado { get; set; }

        [Column("CA_DocRecibida")]																//[varchar](2) NULL,
        public string DocumentacionRecibida { get; set; }

        [Column("CA_Documentacion")]															//[varchar](20) NULL,
        public string Documentacion { get; set; }

        [Column("CA_EstadoMagento")]															//[varchar](15) NULL,
        public string EstadoMagento { get; set; }

        [Column("CA_EstadoVeraz")]																//[varchar](20) NULL,
        public string EstadoVeraz { get; set; }

        [Column("CA_ScoreVeraz")]																//[varchar](10) NULL,
        public string ScoreVeraz { get; set; }

        [Column("CA_Banco")]																	//[varchar](25) NULL,
        public string Banco { get; set; }

        [Column("CA_Tarjeta")]																	//[varchar](100) NULL,
        public string Tarjeta { get; set; }

        [Column("CA_Cuotas")]																	//[varchar](10) NULL,
        public string Cuotas { get; set; }

        [Column("CA_NroLote")]																	//[varchar](10) NULL,
        public string NumeroDeLote { get; set; }

        [Column("CA_NroCupon")]																	//[varchar](10) NULL,
        public string NumeroDeCupon { get; set; }

        [Column("CA_CodAutorizacion")]															//[varchar](15) NULL,
        public string CodigoDeAutorizacion { get; set; }

        [Column("CA_NroTarjeta")]																//[varchar](20) NULL,
        public string NumeroDeTarjeta { get; set; }

        [Column("CA_OrdenMagento")]																//[varchar](15) NULL,
        public string OrdenMagento { get; set; }

        [Column("CA_NPSTransactionID")]															//[varchar](20) NULL,
        public string NpsTransactionID { get; set; }

        [Column("CA_CodRespuestaNPS")]															//[varchar](3) NULL,
        public string CodigoPresupuestoNPS { get; set; }

        [Column("CA_ImporteOriginal", TypeName = "decimal(18,2)")]								//[decimal](18, 2) NULL,
        public decimal? ImporteOriginal { get; set; }

        [Column("CA_NPSMsgRespuesta")]															//[varchar](255) NULL,
        public string NpsMsgRespuesta { get; set; }


    }

}
