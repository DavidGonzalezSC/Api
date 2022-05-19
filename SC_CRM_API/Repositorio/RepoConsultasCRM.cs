using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoConsultasCRM : IConsultasCRM
    {

        private readonly IServiciosSucursales _sucursales;

        public RepoConsultasCRM(IServiciosSucursales sucursales)
        {
            _sucursales = sucursales;


        }

        public async Task<Sucursal> credencialesAsync(string sucursal)
        {
            Sucursal credenciales = await _sucursales.Sucursal(sucursal);

            if (credenciales != null) //forzar parametros de prueba
                return credenciales;
            else
                return null;
     
        }

        public async Task<ClienteDeConsulta> buscarClientePorRazonSocialAsync(string sucursal, string RazonSocial)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.RazonSocial == RazonSocial.Trim()).FirstOrDefault();

            }

        }

        public async Task<ClienteDeConsulta> buscarClientePorCuitAsync(string sucursal, string cuit)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.Cuit == cuit.Trim()).FirstOrDefault();

            }

        }

        public async Task<bool> clienteExisteAsync(string sucursal, string cuit)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.ClientesDeConsulta.Any(c => c.Cuit == cuit.Trim());

            }
        }

        public async Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialTodosAsync(string sucursal, string cadenaDeBusqueda)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                //var lista = _crmDbContext.ClientesDeConsulta.Where(c => c.RazonSocial.Contains(cadenaDeBusqueda)).ToListAsync();
                var lista = _crmDbContext.ClientesDeConsulta.FromSqlRaw($"select * from CRM_CLIENTES where UPPER(RazonSocial) like '%{cadenaDeBusqueda.ToUpper().Trim()}%'").ToListAsync();
                return lista.Result.OrderBy(r => r.RazonSocial);

            }
        }

        public async Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialQueComienzanConAsync(string sucursal, string cadenaDeBusqueda)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                var lista = _crmDbContext.ClientesDeConsulta.Where(c => c.RazonSocial.StartsWith(cadenaDeBusqueda)).ToListAsync();
                return lista.Result.OrderBy(r => r.RazonSocial);

            }
        }

        public async Task<ClienteDeConsulta> buscarClientePorId(string sucursal, string Id)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == Convert.ToInt32(Id.Trim())).FirstOrDefault();

            }
        }

        public async Task<List<PresupuestoDeConsulta>> buscarPresupuestosPorCliente(string sucursal, string idCliente)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.PresupuestosParaConsulta.Where(c => c.IdCliente == Convert.ToInt32(idCliente.Trim())).ToList();

            }
        }

        public async Task<List<DireccionDeEntregaDeConsulta>> buscarDomiciliosPorCliente(string sucursal, string idCliente)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.DireccionDeEntregaParaConsulta.Where(c => c.IdCliente == Convert.ToInt32(idCliente.Trim())).ToList();

            }
        }

        public async Task<List<DetallesEnTabla>> buscarDetallesPorPresupuesto(string sucursal, string idPresupuesto)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.DetallesParaConsulta.Where(c => c.IdPresupuesto == Convert.ToInt32(idPresupuesto.Trim())).ToList();

            }
        }

        public async Task<List<DetallesConVista>> buscarDetallesPorPresupuestoVista(string sucursal, string idPresupuesto)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.DetallesParaConsultaVista.Where(c => c.IdPresupuesto == Convert.ToInt32(idPresupuesto.Trim())).ToList();

            }
        }

        public async Task<PresupuestoDevueltoDbDto> obtenerPresupuesto(string sucursal, string IdPresupuesto)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            PresupuestoDevueltoDbDto presupuesto = new PresupuestoDevueltoDbDto();


            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                presupuesto.Presupuesto = _crmDbContext.PresupuestosParaConsulta.Where(i => i.IdPresupuesto == Convert.ToInt32(IdPresupuesto)).FirstOrDefault();
                presupuesto.Cliente = _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == presupuesto.Presupuesto.IdCliente).FirstOrDefault();
                presupuesto.DetallesDto = _crmDbContext.DetallesParaConsultaVista
                    .Where(d => d.IdPresupuesto == presupuesto.Presupuesto.IdPresupuesto)
                    .OrderBy(cul => cul.IdOrden)
                    .ToList();
                presupuesto.DireccionesDeEntrega = _crmDbContext.DireccionDeEntregaParaConsulta.Where(e => e.IdCliente == presupuesto.Cliente.IdCliente).ToList();

            }

            return presupuesto;

        }

        public async Task<PedidoDeConsultaDto> buscarPedido(string sucursal, Int16 Talonario, string NroDePedido)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            PedidoDeConsultaDto pedido = new PedidoDeConsultaDto();
            List<PedidoDeConsulta> renglones =  new List<PedidoDeConsulta>();

            NroDePedido = " " + NroDePedido;

            await using (var _crmDbContext = new CrmContexto(credenciales))
                renglones = _crmDbContext.PedidosParaConsulta.Where(ip => ip.Nro_Pedido == NroDePedido && ip.TalonPedido == Talonario).ToList();

            if (renglones.Count <= 0)
                return pedido;

            if(renglones != null)
            {
                pedido.Cabecera.TalonPedido = (short)renglones.First().TalonPedido;
                pedido.Cabecera.Nro_Pedido = renglones.First().Nro_Pedido;
                pedido.Cabecera.Cod_Cliente = renglones.First().Cod_Cliente.Trim();
                pedido.Cabecera.RazonSocial = renglones.First().RazonSocial.Trim();
                pedido.Cabecera.Telefono1 = renglones.First().Telefono1.Trim();
                pedido.Cabecera.Telefono2 = renglones.First().Telefono2.Trim();
                pedido.Cabecera.Domicilio = renglones.First().Domicilio.Trim();
                pedido.Cabecera.CodPostal = renglones.First().CodPostal.Trim();
                pedido.Cabecera.Localidad = renglones.First().Localidad.Trim();
                try
                {
                    if (string.IsNullOrEmpty(renglones.FirstOrDefault().Vendedor))
                        pedido.Cabecera.Vendedor = "";
                    else
                    pedido.Cabecera.Vendedor = renglones.FirstOrDefault().Vendedor.Trim();

                }
                catch (Exception)
                {

                    pedido.Cabecera.Vendedor = "";
                }

                if(string.IsNullOrEmpty(renglones.First().Lista))
                    pedido.Cabecera.Lista = "";
                else
                    pedido.Cabecera.Lista = renglones.First().Lista.Trim();

                pedido.Cabecera.CodSucursal = renglones.First().CodSucursal.Trim();
                pedido.Cabecera.Email = renglones.First().Email.Trim();
                pedido.Cabecera.FechaPedido = (DateTime)renglones.First().FechaPedido;

                if(string.IsNullOrEmpty(renglones.First().Clasificacion))
                    pedido.Cabecera.Clasificacion = "";
                else
                    pedido.Cabecera.Clasificacion = renglones.First().Clasificacion.Trim();


                pedido.Cabecera.Cuit = renglones.First().Cuit.Trim();
                pedido.Cabecera.FechaDeEntrega = (DateTime?)renglones.First().FechaDeEntrega;
                pedido.Cabecera.LeyendaDeEntrega = renglones.First().LeyendaDeEntrega;
                pedido.Cabecera.Comentarios = renglones.First().Comentarios;
                pedido.Cabecera.Transporte = renglones.First().Transporte;
                pedido.Cabecera.Leyenda_1 = renglones.First().Leyenda_1;
                pedido.Cabecera.Leyenda_2 = renglones.First().Leyenda_2;
                pedido.Cabecera.RangoHorario = renglones.First().RangoHorario;
                pedido.Cabecera.Entrega_Calle = renglones.First().Entrega_Calle;
                pedido.Cabecera.Entrega_Numero = renglones.First().Entrega_Numero;
                pedido.Cabecera.Entrega_Piso = renglones.First().Entrega_Piso;
                pedido.Cabecera.Entrega_Depto = renglones.First().Entrega_Depto;
                pedido.Cabecera.Entrega_CP = renglones.First().Entrega_CP;
                pedido.Cabecera.Entrega_Localidad = renglones.First().Entrega_Localidad;
                pedido.Cabecera.Entrega_Provincia = renglones.First().Entrega_Provincia;
                pedido.Cabecera.Entrega_Pais = renglones.First().Entrega_Pais;

                //-- Agregados para Virtual
                pedido.Cabecera.CA_Banco = renglones.First().CA_Banco;
                pedido.Cabecera.CA_CodAutorizacion = renglones.First().CA_CodAutorizacion;
                pedido.Cabecera.CA_CodRespuestaNPS = renglones.First().CA_CodRespuestaNPS;
                pedido.Cabecera.CA_Cuotas = renglones.First().CA_Cuotas;
                pedido.Cabecera.CA_DetallePago = renglones.First().CA_DetallePago;
                pedido.Cabecera.CA_DocRecibida = renglones.First().CA_DocRecibida;
                pedido.Cabecera.CA_Documentacion = renglones.First().CA_Documentacion;
                pedido.Cabecera.CA_EstadoMagento = renglones.First().CA_EstadoMagento;
                pedido.Cabecera.CA_EstadoVeraz = renglones.First().CA_EstadoVeraz;
                pedido.Cabecera.CA_Gateway = renglones.First().CA_Gateway;
                pedido.Cabecera.CA_IdMagento = renglones.First().CA_IdMagento;
                pedido.Cabecera.CA_ImporteOriginal = renglones.First().CA_ImporteOriginal;
                pedido.Cabecera.CA_MailEnviado = renglones.First().CA_MailEnviado;
                pedido.Cabecera.CA_NPSMsgRespuesta = renglones.First().CA_NPSMsgRespuesta;
                pedido.Cabecera.CA_NPSTransactionID = renglones.First().CA_NPSTransactionID;
                pedido.Cabecera.CA_NroCupon = renglones.First().CA_NroCupon;
                pedido.Cabecera.CA_NroLote = renglones.First().CA_NroLote;
                pedido.Cabecera.CA_NroTarjeta = renglones.First().CA_NroTarjeta;
                pedido.Cabecera.CA_OrdenMagento = renglones.First().CA_OrdenMagento;
                pedido.Cabecera.CA_ScoreVeraz = renglones.First().CA_ScoreVeraz;
                pedido.Cabecera.CA_Tarjeta = renglones.First().CA_Tarjeta;
                pedido.Cabecera.CA_WowRepe = renglones.First().CA_WowRepe;

                foreach (var item in renglones)
                {

                    PedidoDtoDetalle nuevoRenglon = new PedidoDtoDetalle();
                    nuevoRenglon.Renglon = (int)item.Renglon;
                    nuevoRenglon.Cod_articulo = item.Cod_articulo;
                    nuevoRenglon.Descripcion = item.Descripcion;
                    nuevoRenglon.Descripcion_Adicional = item.Descripcion_Adicional;
                    nuevoRenglon.Cantidad = item.Cantidad;
                    nuevoRenglon.Descuento = item.Descuento;
                    nuevoRenglon.Precio = item.Precio;

                    if(item.IncluyeIva == null)
                        nuevoRenglon.IncluyeIva = false;
                    else
                        nuevoRenglon.IncluyeIva = (bool)item.IncluyeIva;

                    nuevoRenglon.DescripcionDelPedido = item.DescripcionDelPedido;
                    nuevoRenglon.DescripcionDelPedidoAdicional = item.DescripcionDelPedidoAdicional;
                    nuevoRenglon.MedidaEspecial_Ancho = item.MedidaEspecial_Ancho;
                    nuevoRenglon.MedidaEspecial_Largo = item.MedidaEspecial_Largo;
                    nuevoRenglon.MedidaEspecial_Espesor = item.MedidaEspecial_Espesor;

                    pedido.Detalles.Add(nuevoRenglon);
                }
            }

            return pedido;


        }

        public async Task<IEnumerable<PresupuestoPasadosAPedido>> listadoPresupuestoPasadosAPedido(string sucursal, string nroPedido)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            var IdPedido = Convert.ToInt32(nroPedido);
            List<PresupuestoPasadosAPedido> listado = new List<PresupuestoPasadosAPedido>();

            await using (var _crmDbContext = new CrmContexto(credenciales))
                listado = _crmDbContext.PresupuestosAPedidos.Where(ip => ip.Id_Presupuesto == IdPedido).ToList();

            return listado;

        }

        public async Task<PedidoDetalle> buscarPedidoConDetalle(string sucursal, short Talonario, string NroDePedido)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            var pedido = new PedidoDetalle();
            if(!NroDePedido.StartsWith(" "))
                NroDePedido = " " + NroDePedido;

            await using (var _crmDbContext = new CrmContexto(credenciales))
                pedido = _crmDbContext.DetallesPedidos.Where(ip => ip.NRO_PEDIDO == NroDePedido && ip.Talon_ped == Talonario).FirstOrDefault();

            return pedido;
        }

        public async Task<ClienteDeConsulta> buscarClientePorTango(string sucursal, string tango)
        {
            Sucursal contexto = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                return _crmDbContext.ClientesDeConsulta.Where(c => c.CodTango == tango.Trim()).FirstOrDefault();
            }
        }

        public async Task<PresupuestoDevueltoDbDto> obtenerPresupuestoPorMagento(string sucursal, string magento)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            PresupuestoDevueltoDbDto presupuesto = new PresupuestoDevueltoDbDto();


            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                presupuesto.Presupuesto = _crmDbContext.PresupuestosParaConsulta.Where(i => i.OrdenMagento == magento.Trim()).FirstOrDefault();
                presupuesto.Cliente = _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == presupuesto.Presupuesto.IdCliente).FirstOrDefault();
                presupuesto.DetallesDto = _crmDbContext.DetallesParaConsultaVista
                    .Where(d => d.IdPresupuesto == presupuesto.Presupuesto.IdPresupuesto)
                    .OrderBy(cul => cul.IdOrden)
                    .ToList();
                presupuesto.DireccionesDeEntrega = _crmDbContext.DireccionDeEntregaParaConsulta.Where(e => e.IdCliente == presupuesto.Cliente.IdCliente).ToList();

            }

            return presupuesto;
        }

        public async Task<PresupuestoDevueltoDbDto> obtenerPresupuestoPorPedido(string sucursal, string pedido)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            PresupuestoDevueltoDbDto presupuesto = new PresupuestoDevueltoDbDto();

            if (!pedido.StartsWith(" "))
                pedido = " " + pedido;


            await using (var _crmDbContext = new CrmContexto(credenciales))
            {

                var IdPresupuesto = _crmDbContext.PresupuestosAPedidos.Where(ip => ip.Nro_Pedido == pedido).FirstOrDefault().Id_Presupuesto;

                presupuesto.Presupuesto = _crmDbContext.PresupuestosParaConsulta.Where(i => i.IdPresupuesto == IdPresupuesto).FirstOrDefault();
                presupuesto.Cliente = _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == presupuesto.Presupuesto.IdCliente).FirstOrDefault();
                presupuesto.DetallesDto = _crmDbContext.DetallesParaConsultaVista
                    .Where(d => d.IdPresupuesto == presupuesto.Presupuesto.IdPresupuesto)
                    .OrderBy(cul => cul.IdOrden)
                    .ToList();
                presupuesto.DireccionesDeEntrega = _crmDbContext.DireccionDeEntregaParaConsulta.Where(e => e.IdCliente == presupuesto.Cliente.IdCliente).ToList();

            }

            return presupuesto;
        }

        public async Task<List<HistorialMagento>> historialMagento(string sucursal, string magento)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            List<HistorialMagento> historial = new List<HistorialMagento>();

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                historial = await _crmDbContext.HistorialMagentos.Where(m => m.Orden == magento).ToListAsync();

            }

            return historial;
        }

        public async Task<List<HistorialMagento>> presupuestosMagento(string sucursal, string magento)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);
            List<HistorialMagento> historial = new List<HistorialMagento>();
            List<PresupuestoDeConsulta> presus = new List<PresupuestoDeConsulta>();

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                presus = await _crmDbContext.PresupuestosParaConsulta.Where(m => m.OrdenMagento == magento).ToListAsync();

            }

            foreach (var item in presus)
            {
                HistorialMagento tempo = new HistorialMagento();
                tempo.Orden = item.OrdenMagento;
                tempo.Presupuesto = item.IdPresupuesto;
                tempo.Documentacion = item.Documentacion;
                historial.Add(tempo);
            }

            return historial;
        }
    }
}
