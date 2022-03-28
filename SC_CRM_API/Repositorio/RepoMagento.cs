using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Hubs;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoMagento : IMagento
    {
        private readonly MagentoDbContext _ordenesMagento;
        private readonly IHubContext<HubMagento> _hubContext;

        public RepoMagento(MagentoDbContext ordenes, IHubContext<HubMagento> hubcontext)
        {
            _ordenesMagento = ordenes;
            _hubContext = hubcontext;
        }

        public async Task<int> EscribirOrdenV1(EstatusMagentoV1 magento)
        {
            var escritura = 0;

            try
            {
                magento.Procesado = "N";
                magento.AudIngresado = DateTime.Now;
                await _ordenesMagento.DbMagentoStatusV1.AddAsync(magento);
                escritura = await _ordenesMagento.SaveChangesAsync();

            }
            catch (Exception bun)
            {
                escritura = -1;
            }
            return escritura;
        }

        public Task<List<EstatusMagentoV1>> OrdenesCanceladasV1()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstatusMagentoV1>> OrdenesDeUltimas24HorasV1()
        {
            var listado = await _ordenesMagento.DbMagentoStatusV1
                .Where(f => f.AudIngresado.AddDays(1) >= DateTime.Now)
                .OrderByDescending(a => a.AudIngresado).ToListAsync();
            return listado;
        }



        public Task<List<EstatusMagentoV1>> OrdenesProcessingV1()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstatusMagentoV1>> OrdenesSinProcesarV1()
        {
            var listado = await _ordenesMagento.DbMagentoStatusV1.Where(o => o.Procesado == "N").ToListAsync();
            return listado;

        }

        public async Task<List<EstatusMagentoV2>> OrdenesSinProcesarV2()
        {
            var listado = await _ordenesMagento.DbMagentoStatusV2.Where(o => o.Procesado == "N").ToListAsync();
            return listado;
        }

        public async Task<List<EstatusMagentoV2>> OrdenesDeUltimas24HorasV2()
        {
            var listado = await _ordenesMagento.DbMagentoStatusV2
                .Where(f => f.AudIngresado.AddDays(1) >= DateTime.Now)
                .OrderByDescending(a => a.AudIngresado).ToListAsync();
            return listado;
        }

        //--Ejecutar un comando??
        static int RecolectarDatosDeOrdenesAsync(string orden, string estatus)
        {

            ProcessStartInfo processInfo;
            Process proceso;

            string parametros = $"{orden} {estatus}";
            string cadenaEjecutoria = AppDomain.CurrentDomain.BaseDirectory + $"\\ProcesarOrdenesMagento\\ApiOrdenes.exe";
            processInfo = new ProcessStartInfo(cadenaEjecutoria, parametros);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            //-- Redireccionar la salida estandard por ahora..luego a un log o algo
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            proceso = Process.Start(processInfo);

            //-- Capturar la Salidas
            proceso.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("Salida ->" + e.Data);
            proceso.BeginOutputReadLine();

            proceso.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("Errores->" + e.Data);
            proceso.BeginErrorReadLine();

            proceso.WaitForExit();


            Console.WriteLine("CodSalida: {0}", proceso.ExitCode);
            proceso.Close();

            return 1;

        }

        public async Task<int> ObtenerDatosExternos(string numeroDeOrden)
        {
            var ordenAProcesar = await _ordenesMagento.DbMagentoStatusV2.Where(o => o.NroOrden == numeroDeOrden).ToListAsync();

            int cuentaProcesados = 0;

            foreach (var item in ordenAProcesar)
            {
                if (item.Preprocesado == null || item.Preprocesado == false)
                {
                    cuentaProcesados += RecolectarDatosDeOrdenesAsync(item.NroOrden, item.Estado);
                }
            }

            return cuentaProcesados;
        }
    }
}
