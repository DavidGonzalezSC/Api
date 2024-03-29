﻿using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SC_CRM_API.Interfaces
{
    public interface IConsultasCRM
    {
        //-- CLIENTES --

        Task<bool> clienteExisteAsync(string sucursal, string cuit);        //listo
        Task<ClienteDeConsulta> buscarClientePorCuitAsync(string sucursal, string cuit);        //listo
        Task<ClienteDeConsulta> buscarClientePorId(string sucursal, string Id);        //listo
        Task<ClienteDeConsulta> buscarClientePorTango(string sucursal, string tango);
        Task<ClienteDeConsulta> buscarClientePorRazonSocialAsync(string sucursal, string RazonSocial); //--listo
        Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialTodosAsync(string sucursal, string cadenaDeBusqueda); //--Listo
        Task<IEnumerable<ClienteDeConsulta>> buscarClientePorRazonSocialQueComienzanConAsync(string sucursal, string cadenaDeBusqueda); //--Listo


        
        //--PRESUPUESTO
        Task<List<PresupuestoDeConsulta>> buscarPresupuestosPorCliente(string sucursal, string idCliente);
        Task<List<DireccionDeEntregaDeConsulta>> buscarDomiciliosPorCliente(string sucursal, string idCliente);
        Task<List<DetallesEnTabla>> buscarDetallesPorPresupuesto(string sucursal, string idPresupuesto);
        Task<List<DetallesConVista>> buscarDetallesPorPresupuestoVista(string sucursal, string idPresupuesto);
        Task<PresupuestoDevueltoDbDto> obtenerPresupuesto(string sucursal, string IdPresupuesto);
        Task<PresupuestoDevueltoDbDto> obtenerPresupuestoPorMagento(string sucursal, string magento);
        Task<PresupuestoDevueltoDbDto> obtenerPresupuestoPorPedido(string sucursal, string pedido);
        //-- Todo: PRESUPUESTO POR NUMERO



        //--PEDIDO
        Task<PedidoDeConsultaDto> buscarPedido(string sucursal, Int16 Talonario, string NroDePedido);
        Task<PedidoDetalle> buscarPedidoConDetalle(string sucursal, Int16 Talonario, string NroDePedido);
        Task<IEnumerable<PresupuestoPasadosAPedido>> listadoPresupuestoPasadosAPedido(string sucursal, string nroPedido);
        // pedidos por presu
        //   pedidos por cliente



        //--Misc
        Task<List<HistorialMagento>> historialMagento(string sucursal, string magento);
        Task<List<HistorialMagento>> presupuestosMagento(string sucursal, string magento);


        //--MELI
        Task<List<Meli_Auxiliar_V2>> ObtenerMelisNoProcesados();
        Task<Meli_Auxiliar_V2> ObtenerMeliPorOrden(string orden);
        Task<int> ActualizarMeli(Meli_Auxiliar_V2 meli);
        Task<List<ListaDePreciosMeli>> ListaDePreciosVigente();
        Task<List<MeliStock>> StockPorDepo();
        Task<List<ProveedorArtSimple>> ProveedorPorArticulo();







    }
}
