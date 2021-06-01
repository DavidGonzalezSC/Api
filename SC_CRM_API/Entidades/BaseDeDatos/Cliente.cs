using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{

   

    public class ValidarCliente : AbstractValidator<Cliente>
    {
        public ValidarCliente()
        {
            /*
            RuleFor(c => c.IdCliente).NotNull().Equal(0);
            RuleFor(c => c.RazonSocial).NotNull().NotEmpty().MaximumLength(60);
            RuleFor(c => c.NombreCorto).NotNull().MaximumLength(100);
            RuleFor(c => c.NombreFantasia).NotNull().MaximumLength(60);
            RuleFor(c => c.Domicilio).NotNull().MaximumLength(50);
            RuleFor(c => c.Localidad).NotNull().MaximumLength(30);
            RuleFor(c => c.Provincia).NotNull().MaximumLength(2);
            RuleFor(c => c.CodPostal).NotNull().MaximumLength(8);
            RuleFor(c => c.Zona).NotNull().MaximumLength(2);
            RuleFor(c => c.HorarioAten).NotNull().MaximumLength(50);
            RuleFor(c => c.FechaAlta).NotNull();
            RuleFor(c => c.FechaUltimaModificacion).NotNull();
            RuleFor(c => c.ID_VENDEDUltimaModificacion).NotNull();
            RuleFor(c => c.ID_VENDEDAlta).NotNull();
            //RuleFor(c => c.ID_Referencia).NotNull();
            RuleFor(c => c.ComentarioReferencia).NotNull().MaximumLength(50);
            RuleFor(c => c.Telefono).NotNull().MaximumLength(100);
            RuleFor(c => c.Fax).NotNull().MaximumLength(100);
            RuleFor(c => c.Email).NotNull().MaximumLength(255).EmailAddress();
            RuleFor(c => c.MailDe).NotNull().MaximumLength(255);
            RuleFor(c => c.PaginaWeb).NotNull().MaximumLength(70);
            RuleFor(c => c.ListaDePrecios).NotNull();
            RuleFor(c => c.CondicionDeVenta).NotNull();
            RuleFor(c => c.Bonificacion).NotNull().ScalePrecision(2,4);
            RuleFor(c => c.TipoDeDoc).NotNull();
            RuleFor(c => c.Cuit).NotNull().MaximumLength(14);
            RuleFor(c => c.TipoDeIva).NotNull().MaximumLength(3);
            RuleFor(c => c.CupoCredi).NotNull();
            RuleFor(c => c.CodTango).NotNull().MaximumLength(6);
            RuleFor(c => c.Estado).NotNull().MaximumLength(3);
            RuleFor(c => c.Comentarios).NotNull().MaximumLength(2000);
            RuleFor(c => c.Marca_1).NotNull();
            RuleFor(c => c.Marca_2).NotNull();
            RuleFor(c => c.Marca_3).NotNull();
            RuleFor(c => c.Marca_4).NotNull();
            RuleFor(c => c.Marca_5).NotNull();
            RuleFor(c => c.Marca_6).NotNull();
            RuleFor(c => c.Marca_7).NotNull();
            RuleFor(c => c.Marca_8).NotNull();
            RuleFor(c => c.Marca_9).NotNull();
            RuleFor(c => c.Marca_10).NotNull();
            RuleFor(c => c.Marca_11).NotNull();
            RuleFor(c => c.Marca_12).NotNull();
            RuleFor(c => c.Marca_13).NotNull();
            RuleFor(c => c.Marca_14).NotNull();
            RuleFor(c => c.Marca_15).NotNull();
            RuleFor(c => c.Marca_16).NotNull();
            RuleFor(c => c.Marca_17).NotNull();
            RuleFor(c => c.Marca_18).NotNull();
            RuleFor(c => c.Marca_19).NotNull();
            RuleFor(c => c.Marca_20).NotNull();
            RuleFor(c => c.CodTransp).NotNull().MaximumLength(10);
            RuleFor(c => c.porcHabitualTalonario2).NotNull();
            RuleFor(c => c.DomicilioCalle).NotNull().MaximumLength(50);
            RuleFor(c => c.DomicilioNumero).NotNull().MaximumLength(10);
            RuleFor(c => c.DomicilioPiso).NotNull().MaximumLength(10);
            RuleFor(c => c.DomicilioDepto).NotNull().MaximumLength(10);
            RuleFor(c => c.Latitud).NotNull();
            RuleFor(c => c.Longitud).NotNull();
            RuleFor(c => c.DomicilioComercialCalle).NotNull().MaximumLength(50);
            RuleFor(c => c.DomicilioComercialNumero).NotNull().MaximumLength(10);
            RuleFor(c => c.DomicilioComercialPiso).NotNull().MaximumLength(10);
            RuleFor(c => c.DomicilioComercialDepto).NotNull().MaximumLength(10);
            RuleFor(c => c.CodVendedor).NotNull().MaximumLength(10);
            //-- FOTO NO ESTA VALIDADO
            RuleFor(c => c.Campo_1).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_2).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_3).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_4).NotNull().MaximumLength(1000);
            RuleFor(c => c.Campo_5).NotNull().MaximumLength(1000);
            RuleFor(c => c.ComentInha).NotNull().MaximumLength(1000);
            RuleFor(c => c.CobranzaLunesI).NotNull();
            RuleFor(c => c.CobranzaLunesF).NotNull();
            RuleFor(c => c.CobranzaMartesI).NotNull();
            RuleFor(c => c.CobranzaMartesF).NotNull();
            RuleFor(c => c.CobranzaMiercolesI).NotNull();
            RuleFor(c => c.CobranzaMiercolesF).NotNull();
            RuleFor(c => c.CobranzaJuevesI).NotNull();
            RuleFor(c => c.CobranzaJuevesF).NotNull();
            RuleFor(c => c.CobranzaViernesI).NotNull();
            RuleFor(c => c.CobranzaViernesF).NotNull();
            RuleFor(c => c.CobranzaSabadoI).NotNull();
            RuleFor(c => c.CobranzaSabadoF).NotNull();
            RuleFor(c => c.CobranzaDomingoI).NotNull();
            RuleFor(c => c.CobranzaDomingoF).NotNull();
            RuleFor(c => c.CodSucursal).NotNull().MaximumLength(2);
            RuleFor(c => c.LocalidadComercial).NotNull().MaximumLength(30);
            RuleFor(c => c.ProvinciaComercial).NotNull().MaximumLength(2);
            RuleFor(c => c.CodigoPosComercial).NotNull().MaximumLength(8);
            RuleFor(c => c.IdCalleDomicilio).NotNull();
            RuleFor(c => c.IdCalleComercual).NotNull();
            RuleFor(c => c.IdTipoDocumentoExterior).NotNull();
            RuleFor(c => c.NumeroDocumentoExterior).NotNull().MaximumLength(20);
            RuleFor(c => c.CodProvee).NotNull().MaximumLength(8);
            RuleFor(c => c.CaSocioAA).NotNull().MaximumLength(12);
            */
         
        }
    }
    
    [Table("CRM_CLIENTES_TEMP")]
    public class ClienteTemporal
    {
        public ClienteTemporal()
        {
            
        }

        public ClienteTemporal(string sucursal, Guid idEvento)
        {
            Sucursal = sucursal;
            IdEvento = idEvento;
        }

        [Key]
        [Required]
        [Column("ID_Cliente_Temporal")]
        public int IdTemp { get; set; }

        public string Sucursal { get; set; }

        [Column("ID_Evento")]
        public Guid IdEvento { get; set; }

        public int Creacion { get; set; }

    }
    
    public class Cliente : ClienteTemporal
    {
        public Cliente()
        {

        }

        public Cliente(string sucursal, Guid idEvento) : base(sucursal, idEvento)
        {
        }

        [Column("ID_Cliente")]                                                               //[int] NOT NULL,
        public int IdCliente { get; set; }

        [Column("RazonSocial")]
        [DataType(DataType.Text)]                                                           //[varchar](60) NOT NULL,
        public string RazonSocial { get; set; }

        [Column("NombreCorto")]
        [DataType(DataType.Text)]                                                           //[varchar](20) NOT NULL,
        public string NombreCorto { get; set; }

        [Column("NombreFantasia")]
        [DataType(DataType.Text)]                                                           //[varchar](60) NOT NULL,
        public string NombreFantasia { get; set; }

        [Column("Domicilio")]
        [DataType(DataType.Text)]                                                           //[varchar](50) NOT NULL,
        public string Domicilio { get; set; }
        
        [Column("Localidad")]
        [DataType(DataType.Text)]                                                           //[varchar](30) NOT NULL,
        public string Localidad { get; set; }

        [Column("Provincia")]
        [DataType(DataType.Text)]                                                           //[varchar](2) NOT NULL,
        public string Provincia { get; set; }

        [Column("CP")]
        [DataType(DataType.Text)]                                                           //[varchar](8) NOT NULL,
        public string CodPostal { get; set; }

        [Column("Zona")]
        [DataType(DataType.Text)]                                                          //[varchar](2) NOT NULL,
        public string Zona { get; set; }

        [Column("HorarioAten")]
        [DataType(DataType.Text)]                                                           //[varchar](50) NOT NULL,
        public string HorarioAten { get; set; }

        [Column("FechaAlta")]
        [DataType(DataType.DateTime)]                                                       //[datetime] NOT NULL,
        public DateTime? FechaAlta { get; set; }

        [Column("FechaUltimaModificacion")]
        [DataType(DataType.DateTime)]                                                       //[datetime] NOT NULL,
        public DateTime? FechaUltimaModificacion { get; set; }

        
        [Column("ID_VENDEDUltimaModificacion")]                                             //[int] NOT NULL,
        public int ID_VENDEDUltimaModificacion { get; set; }

        
        [Column("ID_VENDEDAlta")]                                                           //[int] NOT NULL,
        public int ID_VENDEDAlta { get; set; }

        [Column("ID_Referencia")]                                                           //[int] NULL,
        public int ID_Referencia { get; set; }

        [Column("ComentarioReferencia")]
        [DataType(DataType.Text)]                                                           //[varchar](50) NOT NULL,
        public string ComentarioReferencia { get; set; }

        [Column("Telef1")]
        [DataType(DataType.Text)]                                                           //[varchar](100) NOT NULL,
        public string Telefono { get; set; }


        [Column("Fax1")]
        [DataType(DataType.Text)]                                                           //[varchar](100) NOT NULL,
        public string Fax { get; set; }

        [Column("Email")]
        [DataType(DataType.Text)]                                                           //[varchar](255) NOT NULL,
        public string Email { get; set; }

        [Column("MAIL_DE")]
        [DataType(DataType.Text)]                                                           //[varchar](255) NOT NULL,
        public string MailDe { get; set; }

        [Column("PagWeb")]
        [DataType(DataType.Text)]                                                                   //[varchar](70) NOT NULL,
        public string PaginaWeb { get; set; }

        [Column("ListaPrecios")]                                                                    //[int] NULL,
        public int ListaDePrecios { get; set; }

        [Column("Cond_Vta")]                                                                        //[int] NULL,
        public int CondicionDeVenta { get; set; }

        
        [Column("Bonif", TypeName = "decimal(4,2)")]                                                //[decimal](4, 2) NOT NULL,
        public decimal Bonificacion { get; set; }

        [Column("TIPO_DOC")]                                                                        //[int] NULL,
        public int TipoDeDoc { get; set; }

        [Column("CUIT")]
        [DataType(DataType.Text)]                                                                   //[varchar](14) NOT NULL,
        public string Cuit { get; set; }

        [Column("TipoIVA")]
        [DataType(DataType.Text)]                                                                   //[varchar](3) NOT NULL,
        public string TipoDeIva { get; set; }

        
        [Column("CUPO_CREDI")]                                                                      //[float] NOT NULL,
        public float CupoCredi { get; set; }

        [Column("Cod_Tango")]
        [DataType(DataType.Text)]                                                                   //[varchar](6) NOT NULL,
        public string CodTango { get; set; }

        [Column("Estado")]
        [DataType(DataType.Text)]                                                                   //[varchar](3) NOT NULL,
        public string Estado { get; set; }

        [Column("Comentarios")]
        [DataType(DataType.Text)]                                                                    //[varchar](2000) NOT NULL,
        public string Comentarios { get; set; }

        [Column("Marca1")]
        public bool Marca_1 { get; set; }
        
        [Column("Marca2")]                                                                          //[bit] NOT NULL,
        public bool Marca_2 { get; set; }
        
        [Column("Marca3")]                                                                          //[bit] NOT NULL,
        public bool Marca_3 { get; set; }

        [Column("Marca4")]                                                                          //[bit] NOT NULL,
        public bool Marca_4 { get; set; }

        [Column("Marca5")]                                                                          //[bit] NOT NULL,
        public bool Marca_5 { get; set; }

        [Column("Marca6")]                                                                          //[bit] NOT NULL,
        public bool Marca_6 { get; set; }

        [Column("Marca7")]                                                                          //[bit] NOT NULL,
        public bool Marca_7 { get; set; }

        [Column("Marca8")]                                                                          //[bit] NOT NULL,
        public bool Marca_8 { get; set; }

        [Column("Marca9")]                                                                          //[bit] NOT NULL,
        public bool Marca_9 { get; set; }

        [Column("Marca10")]                                                                         //[bit] NOT NULL,
        public bool Marca_10 { get; set; }

        [Column("Marca11")]                                                                         //[bit] NOT NULL,
        public bool Marca_11 { get; set; }

        [Column("Marca12")]                                                                         //[bit] NOT NULL,
        public bool Marca_12 { get; set; }

        [Column("Marca13")]                                                                         //[bit] NOT NULL,
        public bool Marca_13 { get; set; }

        [Column("Marca14")]                                                                         //[bit] NOT NULL,
        public bool Marca_14 { get; set; }

        [Column("Marca15")]                                                                         //[bit] NOT NULL,
        public bool Marca_15 { get; set; }

        [Column("Marca16")]                                                                         //[bit] NOT NULL,
        public bool Marca_16 { get; set; }
        
        [Column("Marca17")]                                                                         //[bit] NOT NULL,
        public bool Marca_17 { get; set; }
        
        [Column("Marca18")]                                                                         //[bit] NOT NULL,
        public bool Marca_18 { get; set; }

        [Column("Marca19")]                                                                         //[bit] NOT NULL,
        public bool Marca_19 { get; set; }

        [Column("Marca20")]                                                                         //[bit] NOT NULL,
        public bool Marca_20 { get; set; }

        [Column("COD_TRANSP")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string CodTransp { get; set; }

        [Column("porcHabitualTalonario2")]                                                          //[float] NOT NULL,
        public float porcHabitualTalonario2 { get; set; }

        [Column("CALLE")]
        [DataType(DataType.Text)]                                                                   //[varchar](50) NOT NULL,
        public string DomicilioCalle { get; set; }

        [Column("NRO_DOMIC")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioNumero { get; set; }

        [Column("PISO")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioPiso { get; set; }

        [Column("DEPARTAMENTO_DOMIC")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioDepto { get; set; }
        
        [Column("Latitud")]                                                                         //[float] NOT NULL,
        public float Latitud { get; set; }
        
        [Column("Longitud")]                                                                        //[float] NOT NULL,
        public float Longitud { get; set; }


        [Column("CALLE_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](50) NOT NULL,
        public string DomicilioComercialCalle { get; set; }

        [Column("NRO_DOMIC_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioComercialNumero { get; set; }

        [Column("PISO_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioComercialPiso { get; set; }

        [Column("DEPARTAMENTO_DOMIC_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioComercialDepto { get; set; }

        [Column("COD_VENDED")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NULL,
        public string CodVendedor { get; set; }

        [Column("FOTO")]                                                                            //[image] NULL,
        public byte[] Foto { get; set; }

        [Column("Campo1")]
        [DataType(DataType.Text)]                                                                   //[varchar](1000) NOT NULL,
        public string Campo_1 { get; set; }

        [Column("Campo2")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string Campo_2 { get; set; }

        [Column("Campo3")]
        [DataType(DataType.Text)]                                                                        //[varchar](1000) NOT NULL,
        public string Campo_3 { get; set; }

        [Column("Campo4")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string Campo_4 { get; set; }

        [Column("Campo5")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string Campo_5 { get; set; }

        [Column("FECHA_INHA")]
        [DataType(DataType.DateTime)]                                                                   //[datetime] NULL,
        public DateTime? FechaInha { get; set; }

        [Column("COMENTARIO_INHA")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string ComentInha { get; set; }

        [Column("HorarioCobranzaLunesInicio")]                                            //[int] NOT NULL,
        public int CobranzaLunesI { get; set; }

        [Column("HorarioCobranzaLunesFin")]                                               //[int] NOT NULL,
        public int CobranzaLunesF { get; set; }

        [Column("HorarioCobranzaMartesInicio")]                                           //[int] NOT NULL,
        public int CobranzaMartesI { get; set; }

        [Column("HorarioCobranzaMartesFin")]                                              //[int] NOT NULL,
        public int CobranzaMartesF { get; set; }

        [Column("HorarioCobranzaMiercolesInicio")]                                        //[int] NOT NULL,
        public int CobranzaMiercolesI { get; set; }

        [Column("HorarioCobranzaMiercolesFin")]                                           //[int] NOT NULL,
        public int CobranzaMiercolesF { get; set; }

        [Column("HorarioCobranzaJuevesInicio")]                                           //[int] NOT NULL,
        public int CobranzaJuevesI { get; set; }

        [Column("HorarioCobranzaJuevesFin")]                                              //[int] NOT NULL,
        public int CobranzaJuevesF { get; set; }

        [Column("HorarioCobranzaViernesInicio")]                                          //[int] NOT NULL,
        public int CobranzaViernesI { get; set; }

        [Column("HorarioCobranzaViernesFin")]                                             //[int] NOT NULL,
        public int CobranzaViernesF { get; set; }

        [Column("HorarioCobranzaSabadoInicio")]                                           //[int] NOT NULL,
        public int CobranzaSabadoI { get; set; }

        [Column("HorarioCobranzaSabadoFin")]                                              //[int] NOT NULL,
        public int CobranzaSabadoF { get; set; }

        [Column("HorarioCobranzaDomingoInicio")]                                          //[int] NOT NULL,
        public int CobranzaDomingoI { get; set; }

        [Column("HorarioCobranzaDomingoFin")]                                             //[int] NOT NULL,
        public int CobranzaDomingoF { get; set; }

        [Column("COD_SUCURS")]
        [DataType(DataType.Text)]                                                                   //[varchar](2) NOT NULL,
        public string CodSucursal { get; set; }

        [Column("Localidad_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](30) NOT NULL,
        public string LocalidadComercial { get; set; }

        [Column("Provincia_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](2) NOT NULL,
        public string ProvinciaComercial { get; set; }

        [Column("CP_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](8) NOT NULL,
        public string CodigoPosComercial { get; set; }

        [Column("ID_CalleDomicilio")]                                                               //[int] NULL,
        public int IdCalleDomicilio { get; set; }

        [Column("ID_CalleComercial")]                                                               //[int] NULL,
        public int IdCalleComercual { get; set; }

        [Column("COD_PROVEE")]
        [DataType(DataType.Text)]                                                                   //[varchar](6) NOT NULL,
        public string CodProvee { get; set; }

        [Column("ID_TIPO_DOCUMENTO_EXTERIOR")]                                                      //[int] NULL,
        public int? IdTipoDocumentoExterior { get; set; }

        [Column("NUMERO_DOCUMENTO_EXTERIOR")]
        [DataType(DataType.Text)]                                                                   //[varchar](20) NOT NULL,
        public string NumeroDocumentoExterior { get; set; }

        [Column("CA_SocioAA")]
        [DataType(DataType.Text)]                                                                   //[varchar](12) NOT NULL,
        public string CaSocioAA { get; set; }
    }

    [Table("CRM_CLIENTES")]
    public class ClienteDeConsulta
    {
        [Key]
        [Column("ID_Cliente")]                                                               //[int] NOT NULL,
        public int IdCliente { get; set; }

        [Column("RazonSocial")]
        [DataType(DataType.Text)]                                                           //[varchar](60) NOT NULL,
        public string RazonSocial { get; set; }

        [Column("NombreCorto")]
        [DataType(DataType.Text)]                                                           //[varchar](20) NOT NULL,
        public string NombreCorto { get; set; }

        [Column("NombreFantasia")]
        [DataType(DataType.Text)]                                                           //[varchar](60) NOT NULL,
        public string NombreFantasia { get; set; }

        [Column("Domicilio")]
        [DataType(DataType.Text)]                                                           //[varchar](50) NOT NULL,
        public string Domicilio { get; set; }

        [Column("Localidad")]
        [DataType(DataType.Text)]                                                           //[varchar](30) NOT NULL,
        public string Localidad { get; set; }

        [Column("Provincia")]
        [DataType(DataType.Text)]                                                           //[varchar](2) NOT NULL,
        public string Provincia { get; set; }

        [Column("CP")]
        [DataType(DataType.Text)]                                                           //[varchar](8) NOT NULL,
        public string CodPostal { get; set; }

        [Column("Zona")]
        [DataType(DataType.Text)]                                                          //[varchar](2) NOT NULL,
        public string Zona { get; set; }

        [Column("HorarioAten")]
        [DataType(DataType.Text)]                                                           //[varchar](50) NOT NULL,
        public string HorarioAten { get; set; }

        [Column("FechaAlta")]
        [DataType(DataType.DateTime)]                                                       //[datetime] NOT NULL,
        public DateTime FechaAlta { get; set; }

        [Column("FechaUltimaModificacion")]
        [DataType(DataType.DateTime)]                                                       //[datetime] NOT NULL,
        public DateTime FechaUltimaModificacion { get; set; }


        [Column("ID_VENDEDUltimaModificacion")]                                             //[int] NOT NULL,
        public int ID_VENDEDUltimaModificacion { get; set; }


        [Column("ID_VENDEDAlta")]                                                           //[int] NOT NULL,
        public int ID_VENDEDAlta { get; set; }

        [Column("ID_Referencia")]                                                           //[int] NULL,
        public int? ID_Referencia { get; set; }

        [Column("ComentarioReferencia")]
        [DataType(DataType.Text)]                                                           //[varchar](50) NOT NULL,
        public string ComentarioReferencia { get; set; }

        [Column("Telef1")]
        [DataType(DataType.Text)]                                                           //[varchar](100) NOT NULL,
        public string Telefono { get; set; }


        [Column("Fax1")]
        [DataType(DataType.Text)]                                                           //[varchar](100) NOT NULL,
        public string Fax { get; set; }

        [Column("Email")]
        [DataType(DataType.Text)]                                                           //[varchar](255) NOT NULL,
        public string Email { get; set; }

        [Column("MAIL_DE")]
        [DataType(DataType.Text)]                                                           //[varchar](255) NOT NULL,
        public string MailDe { get; set; }

        [Column("PagWeb")]
        [DataType(DataType.Text)]                                                                   //[varchar](70) NOT NULL,
        public string PaginaWeb { get; set; }

        [Column("ListaPrecios")]                                                                    //[int] NULL,
        public int? ListaDePrecios { get; set; }

        [Column("Cond_Vta")]                                                                        //[int] NULL,
        public int? CondicionDeVenta { get; set; }

        
        [Column("Bonif", TypeName = "decimal(4,2)")]                                                //[decimal](4, 2) NOT NULL,
        public decimal Bonificacion { get; set; }

        [Column("TIPO_DOC")]                                                                        //[int] NULL,
        public int? TipoDeDoc { get; set; }

        [Column("CUIT")]
        [DataType(DataType.Text)]                                                                   //[varchar](14) NOT NULL,
        public string Cuit { get; set; }

        [Column("TipoIVA")]
        [DataType(DataType.Text)]                                                                   //[varchar](3) NOT NULL,
        public string TipoDeIva { get; set; }

        [NotMapped]
        [Column("CUPO_CREDI")]                                                                      //[float] NOT NULL,
        public float CupoCredi { get; set; }

        [Column("Cod_Tango")]
        [DataType(DataType.Text)]                                                                   //[varchar](6) NOT NULL,
        public string CodTango { get; set; }

        [Column("Estado")]
        [DataType(DataType.Text)]                                                                   //[varchar](3) NOT NULL,
        public string Estado { get; set; }

        [Column("Comentarios")]
        [DataType(DataType.Text)]                                                                    //[varchar](2000) NOT NULL,
        public string Comentarios { get; set; }

        [Column("Marca1")]
        public bool Marca_1 { get; set; }

        [Column("Marca2")]                                                                          //[bit] NOT NULL,
        public bool Marca_2 { get; set; }

        [Column("Marca3")]                                                                          //[bit] NOT NULL,
        public bool Marca_3 { get; set; }

        [Column("Marca4")]                                                                          //[bit] NOT NULL,
        public bool Marca_4 { get; set; }

        [Column("Marca5")]                                                                          //[bit] NOT NULL,
        public bool Marca_5 { get; set; }

        [Column("Marca6")]                                                                          //[bit] NOT NULL,
        public bool Marca_6 { get; set; }

        [Column("Marca7")]                                                                          //[bit] NOT NULL,
        public bool Marca_7 { get; set; }

        [Column("Marca8")]                                                                          //[bit] NOT NULL,
        public bool Marca_8 { get; set; }

        [Column("Marca9")]                                                                          //[bit] NOT NULL,
        public bool Marca_9 { get; set; }

        [Column("Marca10")]                                                                         //[bit] NOT NULL,
        public bool Marca_10 { get; set; }

        [Column("Marca11")]                                                                         //[bit] NOT NULL,
        public bool Marca_11 { get; set; }

        [Column("Marca12")]                                                                         //[bit] NOT NULL,
        public bool Marca_12 { get; set; }

        [Column("Marca13")]                                                                         //[bit] NOT NULL,
        public bool Marca_13 { get; set; }

        [Column("Marca14")]                                                                         //[bit] NOT NULL,
        public bool Marca_14 { get; set; }

        [Column("Marca15")]                                                                         //[bit] NOT NULL,
        public bool Marca_15 { get; set; }

        [Column("Marca16")]                                                                         //[bit] NOT NULL,
        public bool Marca_16 { get; set; }

        [Column("Marca17")]                                                                         //[bit] NOT NULL,
        public bool Marca_17 { get; set; }

        [Column("Marca18")]                                                                         //[bit] NOT NULL,
        public bool Marca_18 { get; set; }

        [Column("Marca19")]                                                                         //[bit] NOT NULL,
        public bool Marca_19 { get; set; }

        [Column("Marca20")]                                                                         //[bit] NOT NULL,
        public bool Marca_20 { get; set; }

        [Column("COD_TRANSP")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string CodTransp { get; set; }

        [NotMapped]
        [Column("porcHabitualTalonario2")]                                                          //[float] NOT NULL,
        public float porcHabitualTalonario2 { get; set; }

        [Column("CALLE")]
        [DataType(DataType.Text)]                                                                   //[varchar](50) NOT NULL,
        public string DomicilioCalle { get; set; }

        [Column("NRO_DOMIC")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioNumero { get; set; }

        [Column("PISO")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioPiso { get; set; }

        [Column("DEPARTAMENTO_DOMIC")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioDepto { get; set; }

        [NotMapped]
        [Column("Latitud")]                                                                         //[float] NOT NULL,
        public float Latitud { get; set; }

        
        [Column("Longitud")]                                                                        //[float] NOT NULL,
        public double Longitud { get; set; }


        [Column("CALLE_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](50) NOT NULL,
        public string DomicilioComercialCalle { get; set; }

        [Column("NRO_DOMIC_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioComercialNumero { get; set; }

        [Column("PISO_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioComercialPiso { get; set; }

        [Column("DEPARTAMENTO_DOMIC_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NOT NULL,
        public string DomicilioComercialDepto { get; set; }

        [Column("COD_VENDED")]
        [DataType(DataType.Text)]                                                                   //[varchar](10) NULL,
        public string CodVendedor { get; set; }

        [Column("FOTO")]                                                                            //[image] NULL,
        public byte[] Foto { get; set; }

        [Column("Campo1")]
        [DataType(DataType.Text)]                                                                   //[varchar](1000) NOT NULL,
        public string Campo_1 { get; set; }

        [Column("Campo2")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string Campo_2 { get; set; }

        [Column("Campo3")]
        [DataType(DataType.Text)]                                                                        //[varchar](1000) NOT NULL,
        public string Campo_3 { get; set; }

        [Column("Campo4")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string Campo_4 { get; set; }

        [Column("Campo5")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string Campo_5 { get; set; }

        [Column("FECHA_INHA")]
        [DataType(DataType.DateTime)]                                                                   //[datetime] NULL,
        public DateTime? FechaInha { get; set; }

        [Column("COMENTARIO_INHA")]
        [DataType(DataType.Text)]                                                                       //[varchar](1000) NOT NULL,
        public string ComentInha { get; set; }

        [Column("HorarioCobranzaLunesInicio")]                                            //[int] NOT NULL,
        public int CobranzaLunesI { get; set; }

        [Column("HorarioCobranzaLunesFin")]                                               //[int] NOT NULL,
        public int CobranzaLunesF { get; set; }

        [Column("HorarioCobranzaMartesInicio")]                                           //[int] NOT NULL,
        public int CobranzaMartesI { get; set; }

        [Column("HorarioCobranzaMartesFin")]                                              //[int] NOT NULL,
        public int CobranzaMartesF { get; set; }

        [Column("HorarioCobranzaMiercolesInicio")]                                        //[int] NOT NULL,
        public int CobranzaMiercolesI { get; set; }

        [Column("HorarioCobranzaMiercolesFin")]                                           //[int] NOT NULL,
        public int CobranzaMiercolesF { get; set; }

        [Column("HorarioCobranzaJuevesInicio")]                                           //[int] NOT NULL,
        public int CobranzaJuevesI { get; set; }

        [Column("HorarioCobranzaJuevesFin")]                                              //[int] NOT NULL,
        public int CobranzaJuevesF { get; set; }

        [Column("HorarioCobranzaViernesInicio")]                                          //[int] NOT NULL,
        public int CobranzaViernesI { get; set; }

        [Column("HorarioCobranzaViernesFin")]                                             //[int] NOT NULL,
        public int CobranzaViernesF { get; set; }

        [Column("HorarioCobranzaSabadoInicio")]                                           //[int] NOT NULL,
        public int CobranzaSabadoI { get; set; }

        [Column("HorarioCobranzaSabadoFin")]                                              //[int] NOT NULL,
        public int CobranzaSabadoF { get; set; }

        [Column("HorarioCobranzaDomingoInicio")]                                          //[int] NOT NULL,
        public int CobranzaDomingoI { get; set; }

        [Column("HorarioCobranzaDomingoFin")]                                             //[int] NOT NULL,
        public int CobranzaDomingoF { get; set; }

        [Column("COD_SUCURS")]
        [DataType(DataType.Text)]                                                                   //[varchar](2) NOT NULL,
        public string CodSucursal { get; set; }

        [Column("Localidad_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](30) NOT NULL,
        public string LocalidadComercial { get; set; }

        [Column("Provincia_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](2) NOT NULL,
        public string ProvinciaComercial { get; set; }

        [Column("CP_Comercial")]
        [DataType(DataType.Text)]                                                                   //[varchar](8) NOT NULL,
        public string CodigoPosComercial { get; set; }

        [Column("ID_CalleDomicilio")]                                                               //[int] NULL,
        public int? IdCalleDomicilio { get; set; }

        [Column("ID_CalleComercial")]                                                               //[int] NULL,
        public int? IdCalleComercual { get; set; }

        [Column("COD_PROVEE")]
        [DataType(DataType.Text)]                                                                   //[varchar](6) NOT NULL,
        public string CodProvee { get; set; }

        [Column("ID_TIPO_DOCUMENTO_EXTERIOR")]                                                      //[int] NULL,
        public int? IdTipoDocumentoExterior { get; set; }

        [Column("NUMERO_DOCUMENTO_EXTERIOR")]
        [DataType(DataType.Text)]                                                                   //[varchar](20) NOT NULL,
        public string NumeroDocumentoExterior { get; set; }

        [Column("CA_SocioAA")]
        [DataType(DataType.Text)]                                                                   //[varchar](12) NOT NULL,
        public string CaSocioAA { get; set; }
    }


}
