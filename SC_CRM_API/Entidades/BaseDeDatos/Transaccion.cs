using System;
using System.Collections.Generic;
using System.Text;

namespace SC_CRM_API.Entidades.BaseDeDatos
{
    public class Transaccion
    {
        private static string _sucursal;
        private static Guid _guid;

        public Transaccion(string sucursal)
        {
            _guid = Guid.NewGuid();
            _sucursal = sucursal;

            IdGlobal = _guid;
            Sucursal = _sucursal;
            ClienteSave = false;
            PresupuestoSave = false;
            DomicEntregaSave = false;
            EscrituraExitosa = false;
            TangoSave = false;
        }

        public bool ClienteSave { get; set; }
        public bool PresupuestoSave { get; set; }
        public bool DomicEntregaSave { get; set; }
        public bool EscrituraExitosa { get; set; }
        public bool TangoSave { get; set; }
        public List<string> ListaDeErrores { get; set; } = new List<string>();
        public List<string> ListaDePedidos { get; set; } = new List<string>();
        public Guid IdGlobal { get; private set; } = _guid;
        public string Sucursal { get; private set; } = _sucursal;
        public Cliente Cliente { get; set; } = new Cliente(_sucursal, _guid);
        public Presupuesto Presupuesto { get; set; } = new Presupuesto(_sucursal, _guid);
        public List<Detalle> Detalles { get; set; } = new List<Detalle>();
        public List<DireccionDeEntrega> DireccionesDeEntrega { get; set; } = new List<DireccionDeEntrega>();
        
    }

    public class TransaccionEscrituraDomicilios
    {
        
        

        public TransaccionEscrituraDomicilios(string sucursal)
        {
            IdGlobal = Guid.NewGuid();
            Sucursal = sucursal;
            EscrituraExitosa = false;

        }

        public int IdCliente { get; set; } = 0;
        public Guid IdGlobal { get; private set; }
        public string Sucursal { get; set; }

        public List<string> ListaDeErrores { get; set; } = new List<string>();
        public List<string> ListaDeDomicilios { get; set; } = new List<string>();
        public bool EscrituraExitosa { get; set; }
        

        public List<DireccionDeEntrega> DireccionesDeEntrega { get; set; } = new List<DireccionDeEntrega>();

    }
}
