using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{

    [Table("CRM_Sucursales_TEMP")]
    public class DireciconDeEntregaTemp
    {
        public DireciconDeEntregaTemp()
        {

        }

        public DireciconDeEntregaTemp(string sucursal, Guid idEvento)
        {
            Sucursal = sucursal;
            IdEvento = idEvento;
        }

        [Key]
        [Required]
        [Column("ID_Sucursal_Temporal")]
        public int IdTemp { get; set; }

        public string Sucursal { get; set; }

        [Column("ID_Evento")]
        public Guid IdEvento { get; set; }

        public int Creacion { get; set; }

    }


    public class ValidarDireccionDeEntrega : AbstractValidator<DireccionDeEntrega>
    {
        public ValidarDireccionDeEntrega()
        {
            /*
            RuleFor(c => c.Id).NotNull().Equal(0);
            RuleFor(c => c.IdCliente).NotNull().Equal(0);
            RuleFor(c => c.Nombre).MaximumLength(50);
            RuleFor(c => c.Direccion).MaximumLength(50);
            RuleFor(c => c.Calle).NotNull().MaximumLength(50);
            RuleFor(c => c.Numero).NotNull().MaximumLength(10);
            RuleFor(c => c.Piso).NotNull().MaximumLength(10);
            RuleFor(c => c.Depto).NotNull().MaximumLength(10);
            RuleFor(c => c.Localidad).NotNull().MaximumLength(30);
            RuleFor(c => c.Provincia).MaximumLength(2);
            RuleFor(c => c.CodPos).MaximumLength(10);
            RuleFor(c => c.Telefono).MaximumLength(50);
            RuleFor(c => c.Fax).MaximumLength(50);
            RuleFor(c => c.Encargado).MaximumLength(50);
            RuleFor(c => c.Horario).MaximumLength(50);
            RuleFor(c => c.Email).MaximumLength(50);
            RuleFor(c => c.IdDireccionEntrega).NotNull();
            RuleFor(c => c.Observaciones).NotNull().MaximumLength(8000);
            RuleFor(c => c.IdCalle).NotNull();
            RuleFor(c => c.AlFijIb3).NotNull();
            RuleFor(c => c.AliAdiIb).NotNull().MaximumLength(2);
            RuleFor(c => c.AliFijIb).NotNull().MaximumLength(2);
            RuleFor(c => c.IbL).NotNull();
            RuleFor(c => c.IbL3).NotNull();
            RuleFor(c => c.IiIb3).NotNull();
            RuleFor(c => c.Lib).NotNull();
            RuleFor(c => c.PorcL).NotNull().ScalePrecision(2, 18);
            RuleFor(c => c.FechaUltimaModificacion);
            RuleFor(c => c.VendedorUltimaModif).NotNull();
            RuleFor(c => c.Lat).NotNull();
            RuleFor(c => c.Long).NotNull();
            */
        }
    }


    public class DireccionDeEntrega : DireciconDeEntregaTemp
    {

        public DireccionDeEntrega()
        {

        }

        public DireccionDeEntrega(string sucursal, Guid idEvento) : base(sucursal, idEvento)
        {
        }

        [Column("ID_Sucursal")]                           //[int] IDENTITY(1,1) NOT NULL,
        public int Id { get; set; }

        [Column("ID_Cliente")]                            //[int] NOT NULL,
		public int IdCliente { get; set; }

        [Column("Nombre")]                                //[varchar](50) NULL,
		public string Nombre { get; set; }

        [Column("Direccion")]                             //[varchar](50) NULL,
		public string Direccion { get; set; }

        [Column("CALLE")]                                 //[varchar](50) NOT NULL,
		public string Calle { get; set; }

        [Column("NRO_DOMIC")]                               //[varchar](10) NOT NULL,
        public string Numero { get; set; }

        [Column("PISO")]                                    //[varchar](10) NOT NULL,
        public string Piso { get; set; }

        [Column("DEPARTAMENTO_DOMIC")]                  //[varchar](10) NOT NULL,
        public string Depto { get; set; }

        [Column("Localidad")]                               //[varchar](30) NOT NULL,
        public string Localidad { get; set; }

        [Column("Provincia")]                               //[varchar](2) NOT NULL,
        public string Provincia { get; set; }

        [Column("C_Postal")]                                //[varchar](10) NULL,
        public string CodPos { get; set; }

        [Column("Telefono")]                                //[varchar](50) NULL,
        public string Telefono { get; set; }

        [Column("Fax")]                                 //[varchar](50) NULL,
        public string Fax { get; set; }

        [Column("Encargado")]								//[varchar](50) NULL,
        public string Encargado { get; set; }

        [Column("Horario")]								//[varchar](50) NULL,
        public string Horario { get; set; }

        [Column("eMail")]									//[varchar](50) NULL,
        public string Email { get; set; }

        [Column("ID_DIRECCION_ENTREGA")]					//[int] NULL,
        public int? IdDireccionEntrega { get; set; }

        [Column("Observaciones")]							//[varchar](8000) NOT NULL,
        public string Observaciones { get; set; }

        [Column("ID_Calle")]								//[int] NULL,
        public int? IdCalle { get; set; }

        [Column("AL_FIJ_IB3")]							//[int] NOT NULL,
        public int AlFijIb3 { get; set; }

        [Column("ALI_ADI_IB")]							//[varchar](2) NOT NULL,
        public string AliAdiIb { get; set; }

        [Column("ALI_FIJ_IB")]							//[varchar](2) NOT NULL,
        public string AliFijIb { get; set; }

        [Column("IB_L")]									// [bit] NOT NULL,
        public bool IbL { get; set; }

        [Column("IB_L3")]									//[bit] NOT NULL,
        public bool IbL3 { get; set; }

        [Column("II_IB3")]								//[bit] NOT NULL,
        public bool IiIb3 { get; set; }

        [Column("LIB")]									//[varchar](1) NOT NULL,
        public string Lib { get; set; }

        [Column("PORC_L")]								//[decimal](18, 2) NOT NULL,
        public decimal PorcL { get; set; }

        [Column("Fecha_UltimaModificacion")]				//[datetime] NULL,
        public DateTime? FechaUltimaModificacion { get; set; }

        [Column("ID_Vendedor_UltimaModificacion")]		//[int] NULL,
        public int VendedorUltimaModif { get; set; }

        [Column("Latitud")]								//[float] NOT NULL,
        public float Lat { get; set; }

        [Column("Longitud")]								//[float] NOT NULL,
        public float Long { get; set; }


        //--AGREGADAS 08/02/2022

        [Column("TienePedido")]
        public int? TienePedido { get; set; }

        [Column("LinkGoogleMaps")]
        public string LinkGoogleMaps { get; set; }

    }


    //--PARA consultas
    [Table("VW_CRM_Sucursales")]
    public class DireccionDeEntregaDeConsulta
    {

        [Column("ID_Sucursal")]                           //[int] IDENTITY(1,1) NOT NULL,
        public int Id { get; set; }

        [Column("ID_Cliente")]                            //[int] NOT NULL,
        public int IdCliente { get; set; }

        [Column("Nombre")]                                //[varchar](50) NULL,
        public string Nombre { get; set; }

        [Column("Direccion")]                             //[varchar](50) NULL,
        public string Direccion { get; set; }

        [Column("CALLE")]                                 //[varchar](50) NOT NULL,
        public string Calle { get; set; }

        [Column("NRO_DOMIC")]                               //[varchar](10) NOT NULL,
        public string Numero { get; set; }

        [Column("PISO")]                                    //[varchar](10) NOT NULL,
        public string Piso { get; set; }

        [Column("DEPARTAMENTO_DOMIC")]                  //[varchar](10) NOT NULL,
        public string Depto { get; set; }

        [Column("Localidad")]                               //[varchar](30) NOT NULL,
        public string Localidad { get; set; }

        [Column("Provincia")]                               //[varchar](2) NOT NULL,
        public string Provincia { get; set; }

        [Column("C_Postal")]                                //[varchar](10) NULL,
        public string CodPos { get; set; }

        [Column("Telefono")]                                //[varchar](50) NULL,
        public string Telefono { get; set; }

        [Column("Fax")]                                 //[varchar](50) NULL,
        public string Fax { get; set; }

        [Column("Encargado")]								//[varchar](50) NULL,
        public string Encargado { get; set; }

        [Column("Horario")]								//[varchar](50) NULL,
        public string Horario { get; set; }

        [Column("eMail")]									//[varchar](50) NULL,
        public string Email { get; set; }

        [Column("ID_DIRECCION_ENTREGA")]					//[int] NULL,
        public int? IdDireccionEntrega { get; set; }

        [Column("Observaciones")]							//[varchar](8000) NOT NULL,
        public string Observaciones { get; set; }

        [Column("ID_Calle")]								//[int] NULL,
        public int? IdCalle { get; set; }

        [Column("AL_FIJ_IB3")]							//[int] NOT NULL,
        public int AlFijIb3 { get; set; }

        [Column("ALI_ADI_IB")]							//[varchar](2) NOT NULL,
        public string AliAdiIb { get; set; }

        [Column("ALI_FIJ_IB")]							//[varchar](2) NOT NULL,
        public string AliFijIb { get; set; }

        [Column("IB_L")]									// [bit] NOT NULL,
        public bool IbL { get; set; }

        [Column("IB_L3")]									//[bit] NOT NULL,
        public bool IbL3 { get; set; }

        [Column("II_IB3")]								//[bit] NOT NULL,
        public bool IiIb3 { get; set; }

        [Column("LIB")]									//[varchar](1) NOT NULL,
        public string Lib { get; set; }

        [Column("PORC_L")]								//[decimal](18, 2) NOT NULL,
        public decimal PorcL { get; set; }

        [Column("Fecha_UltimaModificacion")]				//[datetime] NULL,
        public DateTime? FechaUltimaModificacion { get; set; }

        [Column("ID_Vendedor_UltimaModificacion")]		//[int] NULL,
        public int? VendedorUltimaModif { get; set; }

        [Column("Latitud")]								//[float] NOT NULL,
        public double? Lat { get; set; }

        [Column("Longitud")]								//[float] NOT NULL,
        public double? Long { get; set; }

        [Column("TienePedido")]
        public int TienePedido { get; set; }           //agregado para las consultas de domicilios que tengan asociada esa direcicon a un pedido existente. NO VIVE EN LA TEMPORAL DE ESCRITURA



    }
}
