using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoMagento : IMagento
    {
        private readonly SucursalesDbContext _ordenesMagento;

        public RepoMagento(SucursalesDbContext ordenes)
        {
            _ordenesMagento = ordenes;
        }

        public Task<List<EstatusMagento>> OrdenesCanceladas()
        {
            throw new NotImplementedException();
        }

        public Task<List<EstatusMagento>> OrdenesDeUltimas24Horas()
        {
            throw new NotImplementedException();
        }

        public Task<List<EstatusMagento>> OrdenesProcessing()
        {
            throw new NotImplementedException();
        }

        public async Task<List<EstatusMagento>> OrdenesSinProcesar()
        {
            var listado = await _ordenesMagento.DbMagentoStatus.Where(o => o.Procesado == "N").ToListAsync();
            return listado;

        }
    }
}
