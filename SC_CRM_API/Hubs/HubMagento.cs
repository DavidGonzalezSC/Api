using Microsoft.AspNetCore.SignalR;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Hubs
{
    public class HubMagento : Hub
    {
        private readonly IMagento _magento;

        public HubMagento(IMagento ordenes)
        {
            _magento = ordenes;
        }

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("Conectado", "Esperando Ordenes");

            var listado = await _magento.OrdenesDeUltimas24HorasV1();
            await Clients.Caller.SendAsync("ultimas24hv1", listado);

        }

        public async Task OrdenesDeUltimas24HorasV1(List<EstatusMagentoV1> listado)
        {
            await Clients.All.SendAsync("ultimas24hv1", listado);
        }


        public async Task EnviarOrdenesV2(List<EstatusMagentoV2> listado)
        {
            await Clients.Caller.SendAsync("ObtenerOrdenesV2", listado);
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.Caller.SendAsync("Desconectado", "OK");
            await base.OnDisconnectedAsync(exception);

        }
    }
}
