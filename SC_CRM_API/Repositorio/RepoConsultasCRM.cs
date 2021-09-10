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

        public async Task<List<DetalleDeConsulta>> buscarDetallesPorPresupuesto(string sucursal, string idPresupuesto)
        {
            Sucursal credenciales = await credencialesAsync(sucursal);

            await using (var _crmDbContext = new CrmContexto(credenciales))
            {
                return _crmDbContext.DetallesParaConsulta.Where(c => c.IdPresupuesto == Convert.ToInt32(idPresupuesto.Trim())).ToList();

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
                presupuesto.DetallesDto = _crmDbContext.DetallesParaConsulta.Where(d => d.IdPresupuestoDetalle == presupuesto.Presupuesto.IdPresupuesto).ToList();
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
                pedido.Cabecera.TalonPedido = renglones.First().TalonPedido;
                pedido.Cabecera.Nro_Pedido = renglones.First().Nro_Pedido;
                pedido.Cabecera.Cod_Cliente = renglones.First().Cod_Cliente.Trim();
                pedido.Cabecera.RazonSocial = renglones.First().RazonSocial.Trim();
                pedido.Cabecera.Telefono1 = renglones.First().Telefono1.Trim();
                pedido.Cabecera.Telefono2 = renglones.First().Telefono2.Trim();
                pedido.Cabecera.Domicilio = renglones.First().Domicilio.Trim();
                pedido.Cabecera.CodPostal = renglones.First().CodPostal.Trim();
                pedido.Cabecera.Localidad = renglones.First().Localidad.Trim();
                pedido.Cabecera.Vendedor = renglones.First().Vendedor.Trim();
                pedido.Cabecera.Lista = renglones.First().Lista.Trim();
                pedido.Cabecera.CodSucursal = renglones.First().CodSucursal.Trim();
                pedido.Cabecera.Email = renglones.First().Email.Trim();
                pedido.Cabecera.FechaPedido = renglones.First().FechaPedido;
                pedido.Cabecera.Clasificacion = renglones.First().Clasificacion.Trim();
                pedido.Cabecera.Cuit = renglones.First().Cuit.Trim();
                pedido.Cabecera.FechaDeEntrega = renglones.First().FechaDeEntrega;
                pedido.Cabecera.LeyendaDeEntrega = renglones.First().LeyendaDeEntrega.Trim();
                pedido.Cabecera.Comentarios = renglones.First().Comentarios.Trim();
                pedido.Cabecera.Transporte = renglones.First().Transporte.Trim();
                pedido.Cabecera.Leyenda_1 = renglones.First().Leyenda_1.Trim();
                pedido.Cabecera.Leyenda_2 = renglones.First().Leyenda_2.Trim();
                pedido.Cabecera.RangoHorario = renglones.First().RangoHorario.Trim();
                pedido.Cabecera.Entrega_Calle = renglones.First().Entrega_Calle.Trim();
                pedido.Cabecera.Entrega_Numero = renglones.First().Entrega_Numero.Trim();
                pedido.Cabecera.Entrega_Piso = renglones.First().Entrega_Piso.Trim();
                pedido.Cabecera.Entrega_Depto = renglones.First().Entrega_Depto.Trim();
                pedido.Cabecera.Entrega_CP = renglones.First().Entrega_CP.Trim();
                pedido.Cabecera.Entrega_Localidad = renglones.First().Entrega_Localidad.Trim();
                pedido.Cabecera.Entrega_Provincia = renglones.First().Entrega_Provincia.Trim();
                pedido.Cabecera.Entrega_Pais = renglones.First().Entrega_Pais.Trim();

                foreach (var item in renglones)
                {

                    PedidoDtoDetalle nuevoRenglon = new PedidoDtoDetalle();
                    nuevoRenglon.Renglon = item.Renglon;
                    nuevoRenglon.Cod_articulo = item.Cod_articulo;
                    nuevoRenglon.Descripcion = item.Descripcion;
                    nuevoRenglon.Descripcion_Adicional = item.Descripcion_Adicional;
                    nuevoRenglon.Cantidad = item.Cantidad;
                    nuevoRenglon.Descuento = item.Descuento;
                    nuevoRenglon.Precio = item.Precio;
                    nuevoRenglon.IncluyeIva = item.IncluyeIva;
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
    }
}
