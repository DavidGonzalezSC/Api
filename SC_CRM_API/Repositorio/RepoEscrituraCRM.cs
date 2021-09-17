using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoEscrituraCRM : IEscrituraCRM
    {

        private readonly IServiciosSucursales _sucursales;

        public RepoEscrituraCRM(IServiciosSucursales sucursales)
        {
            _sucursales = sucursales ?? throw new ArgumentNullException(nameof(IServiciosSucursales));
        }

        public void EscribirLogs(string sucursal, string mensaje)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string filepath = AppDomain.CurrentDomain.BaseDirectory + $"\\Logs\\Errores_{sucursal}_" + DateTime.Now.Date.ToShortDateString().Replace('/', '_') + "_" + DateTime.Now.Ticks + ".txt";

            if (!File.Exists(filepath))
            {
                // Create a file to write to.   
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine(mensaje);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine(mensaje);
                }
            }

        }


        public async Task<Sucursal> credencialesAsync(string sucursal)
        {
            Sucursal credenciales = await _sucursales.Sucursal(sucursal);

            if (credenciales != null)
                return credenciales;
            else
                return null;
        }

        public async Task<TransaccionEscrituraDomicilios> EscribirSoloDomicilios(TransaccionEscrituraDomicilios transaccion)
        {
            Sucursal sucursal = await credencialesAsync(transaccion.Sucursal);
            string metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;

            foreach (var direccion in transaccion.DireccionesDeEntrega) //-- Verificar si al menos una tiene el Id de Cliente
            {
                direccion.IdCliente = transaccion.IdCliente;
                direccion.IdEvento = transaccion.IdGlobal;
                direccion.Sucursal = transaccion.Sucursal;
            }

            await using (var contextoDeEscritura = new CrmContexto(sucursal))
            {

                try
                {
                    using (var transaccionDb = contextoDeEscritura.Database.BeginTransaction())
                    {

                        List<SqlRespuestaDomicilios> listaDeEscritoDomicilio = new List<SqlRespuestaDomicilios>();

                        if (transaccion.DireccionesDeEntrega.Count > 0)
                        {
                            foreach (DireccionDeEntrega direccion in transaccion.DireccionesDeEntrega)
                            {
                                contextoDeEscritura.DireccionDeEntregas.Add(direccion);
                            }

                            var salidaCliente = contextoDeEscritura.SaveChanges();
                            //transaccionDb.Commit(); PARA PRUEBAS
                            //return null;

                            if (salidaCliente == transaccion.DireccionesDeEntrega.Count()) //si falla guardar en la temp
                                listaDeEscritoDomicilio = EscribirDomicilioSP(transaccion.IdGlobal, contextoDeEscritura).ToList();
                            else
                                throw new InvalidOperationException("Falló la Escritura de la Tabla Temporal");

                        }
                        else
                        {
                            var escritoDomicilio = new SqlRespuestaDomicilios();
                            escritoDomicilio.Comprobante = "0";
                            listaDeEscritoDomicilio.Add(escritoDomicilio);

                        }

                        if (string.IsNullOrEmpty(listaDeEscritoDomicilio.First().Comprobante))
                        {
                            transaccion.ListaDeErrores.Add($"Llamada: {metodo} - ERROR SQL: BUN");
                            transaccion.EscrituraExitosa = false;
                            transaccionDb.Rollback();
                            transaccionDb.Dispose();

                        }
                        else
                        {
                            foreach (SqlRespuestaDomicilios domicilio in listaDeEscritoDomicilio)
                            {
                                if (domicilio.Resultado.Contains("Error"))
                                {
                                    transaccion.ListaDeErrores.Add($"Llamada: SP de Direcciones - ERROR SQL: {domicilio.Error_Mensaje}");
                                }
                                else
                                {
                                    transaccion.ListaDeDomicilios.Add(domicilio.Comprobante);
                                }

                            }
                            //--escribio todo bien
                            transaccionDb.Commit();
                            transaccion.EscrituraExitosa = true;
                        }
                    }
                }
                catch (DbUpdateException ex)
                {
                    transaccion.EscrituraExitosa = false;
                    transaccion.ListaDeErrores.Add($"Llamada: {metodo} - ERROR SQL: {ex.InnerException.Message}");
                }
                            
            }
            return transaccion;
           
        }

        public async Task<Transaccion> GuardarTransaccionAsyncV2(Transaccion transac, bool PasarAPedido)
        {

            transac.Cliente.Sucursal = transac.Sucursal;
            transac.Cliente.IdEvento = transac.IdGlobal;
            transac.Presupuesto.Sucursal = transac.Sucursal;
            transac.Presupuesto.IdEvento = transac.IdGlobal;

            foreach (var direccion in transac.DireccionesDeEntrega)
            {
                direccion.Sucursal = transac.Sucursal;
                direccion.IdEvento = transac.IdGlobal;

            }

            Sucursal sucursal = await credencialesAsync(transac.Sucursal);
            string metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int salidaCliente = 0;

            await using (var contextoDeEscritura = new CrmContexto(sucursal))
            {

                try
                {
                    using (var transaccion = contextoDeEscritura.Database.BeginTransaction())
                    {

                        contextoDeEscritura.Clientes.Add(transac.Cliente);
                        salidaCliente = contextoDeEscritura.SaveChanges();
                        //--SP de Cliente
                        var escritoCliente = EscribirClienteSP(transac.IdGlobal, contextoDeEscritura);

                        if (string.IsNullOrEmpty(escritoCliente.Comprobante))
                        {
                            transac.ListaDeErrores.Add($"Llamada: {metodo} - ERROR SQL: {escritoCliente.Error_Mensaje}");
                            transac.EscrituraExitosa = false;
                            transaccion.Rollback();
                            transaccion.Dispose();
                        }
                        else
                        {
                            transac.ClienteSave = true;
                            //cliente escribio bien..pasamos a domicilio
                            //--domicilio
                            List<SqlRespuestaDomicilios> listaDeEscritoDomicilio = new List<SqlRespuestaDomicilios>();

                            if (transac.DireccionesDeEntrega.Count > 0)
                            {
                                foreach (DireccionDeEntrega direccion in transac.DireccionesDeEntrega)
                                {
                                    direccion.IdCliente = Convert.ToInt32(escritoCliente.Comprobante); //paso el dato devuelto por el SP
                                    contextoDeEscritura.DireccionDeEntregas.Add(direccion);
                                }

                                salidaCliente = contextoDeEscritura.SaveChanges();
                                listaDeEscritoDomicilio = EscribirDomicilioSP(transac.IdGlobal, contextoDeEscritura).ToList();

                            }else
                            {
                                var escritoDomicilio = new SqlRespuesta();
                                escritoDomicilio.Comprobante = "0";
                                
                            }


                            if (string.IsNullOrEmpty(listaDeEscritoDomicilio.First().Comprobante))
                            {
                                transac.ListaDeErrores.Add($"Llamada: {metodo} - ERROR SQL: BUN");
                                transac.EscrituraExitosa = false;
                                transaccion.Rollback();
                                transaccion.Dispose();

                            }
                            else
                            {


                                //domicilio escribio bien pasamos al presupuesto
                                transac.DomicEntregaSave = true;

                                transac.Presupuesto.IdDeSucursal = Convert.ToInt32(0); //paso el dato del cliente
                                transac.Presupuesto.IdCliente = Convert.ToInt32(escritoCliente.Comprobante); //paso el dato del cliente

                                contextoDeEscritura.Presupuestos.Add(transac.Presupuesto);
                                salidaCliente = contextoDeEscritura.SaveChanges();

                                foreach (Detalle detalle in transac.Detalles)
                                {

                                    detalle.Sucursal = transac.Sucursal;
                                    detalle.IdEvento = transac.IdGlobal;

                                    detalle.CA_IdDireccionEntrega = Convert.ToInt32(listaDeEscritoDomicilio
                                        .Where(d => d.NombreDomicilio == detalle.NombreDomicilio)
                                        .FirstOrDefault().Comprobante.Trim());

                                    contextoDeEscritura.Detalles.Add(detalle);

                                }

                                salidaCliente = contextoDeEscritura.SaveChanges();

                                var escritoPresupuesto = EscribirPresupuestoSP(transac.IdGlobal, contextoDeEscritura);

                                if (string.IsNullOrEmpty(escritoPresupuesto.Comprobante))
                                {
                                    transac.ListaDeErrores.Add($"Llamada: {metodo} - ERROR SQL: {escritoPresupuesto.Error_Mensaje}");
                                    transac.EscrituraExitosa = false;
                                    transaccion.Rollback();
                                    transaccion.Dispose();
                                }
                                else
                                {

                                    transac.PresupuestoSave = true;
                                    transac.EscrituraExitosa = true;

                                    //VERIFICAR VALIDACIONES CON EMA
                                    //verificar la escritura en tango
                                    if (PasarAPedido)
                                    {
                                        var listadoDeTango = EscribirEnTango(transac.IdGlobal, contextoDeEscritura);
                                        transac.TangoSave = true;

                                        foreach (SqlRespuesta pedido in listadoDeTango)
                                        {
                                            if (pedido.Resultado.Contains("Error"))
                                            {
                                                transac.TangoSave = false;
                                                transac.ListaDeErrores.Add($"Llamada: SP de Tango - ERROR SQL: {pedido.Error_Mensaje}");
                                            }else
                                            {
                                                transac.ListaDePedidos.Add(pedido.Comprobante);
                                           
                                            }
                                        
                                        }

                                    }else
                                    {
                                        transac.TangoSave = true;
                                    }
                                    //---------------------------


                                    if (!transac.TangoSave)
                                    {
                                        transac.EscrituraExitosa = false;
                                        transaccion.Rollback();
                                        transaccion.Dispose();

                                    }else
                                    {
                                        transaccion.Commit();
                                    }
                                }

                            }
                        }

                    }

                }
                catch (DbUpdateException ex)
                {
                    transac.EscrituraExitosa = false;
                    transac.ListaDeErrores.Add($"Llamada: {metodo} - ERROR SQL: {ex.InnerException.Message}");
                }

                return transac;


            }
        }


        public async Task<Transaccion> EscribirSoloEnTemporalParaPruebas(Transaccion transac)
        {

            transac.Cliente.Sucursal = transac.Sucursal;
            transac.Cliente.IdEvento = transac.IdGlobal;
            transac.Presupuesto.Sucursal = transac.Sucursal;
            transac.Presupuesto.IdEvento = transac.IdGlobal;
            foreach (var direccion in transac.DireccionesDeEntrega)
            {
                direccion.Sucursal = transac.Sucursal;
                direccion.IdEvento = transac.IdGlobal;

            }

            Sucursal sucursal = await credencialesAsync(transac.Sucursal);
            string metodo = System.Reflection.MethodBase.GetCurrentMethod().Name;
            int salidaCliente = 0;

            await using (var contextoDeEscritura = new CrmContexto(sucursal))
            {

                try
                {
                    using (var transaccion = contextoDeEscritura.Database.BeginTransaction())
                    {

                        //Guardar Cliente en la Temporal
                        contextoDeEscritura.Clientes.Add(transac.Cliente);
                        salidaCliente = contextoDeEscritura.SaveChanges();
                        

                        //-- Guardar Direcciones de entrega
                        foreach (DireccionDeEntrega direccion in transac.DireccionesDeEntrega)
                           contextoDeEscritura.DireccionDeEntregas.Add(direccion);

                        salidaCliente = contextoDeEscritura.SaveChanges();

                        //--Guardo Presupuesto Cabecera
                        contextoDeEscritura.Presupuestos.Add(transac.Presupuesto);
                        salidaCliente = contextoDeEscritura.SaveChanges();

                        //-- Guardo Detalles
                        foreach (Detalle detalle in transac.Detalles)
                        {
                            detalle.Sucursal = transac.Sucursal;
                            detalle.IdEvento = transac.IdGlobal;
                            contextoDeEscritura.Detalles.Add(detalle);
                        }

                        salidaCliente = contextoDeEscritura.SaveChanges();


                        //--Fin de Transaccion
                        transaccion.Commit();
                        transac.EscrituraExitosa = true;



                    }
                        

                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex);
                    transac.EscrituraExitosa = false;

                }

                return transac;


            }
        }

        private IEnumerable<SqlRespuesta> EscribirEnTango(Guid guid, CrmContexto crmContexto)
        {
            var listaDeDatos = new List<SqlRespuesta>();
            SqlRespuesta spClienteTango;
            SqlRespuesta spDireccionTango;
            SqlRespuesta spPresupuestoTango;

            try
            {
                spClienteTango = crmContexto.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_SC_ClienteTango '{guid}';").AsEnumerable().FirstOrDefault();
                listaDeDatos.Add(spClienteTango);

            }
            catch (Exception error)
            {
                spClienteTango = new SqlRespuesta
                {
                    Resultado = "Error: Catch Tango Cliente",
                    Comprobante = "",
                    Fecha = DateTime.Now,
                    Error_Mensaje = error.Message,
                    Error_Severidad = "0",
                    Error_Estado = "0"
                };

                listaDeDatos.Add(spClienteTango);

            }

            try
            {
                spDireccionTango = crmContexto.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_SC_DIRECCION_ENTREGA_Tango '{guid}';").AsEnumerable().FirstOrDefault();
                listaDeDatos.Add(spDireccionTango);

            }
            catch (Exception error)
            {
                spDireccionTango = new SqlRespuesta
                {
                    Resultado = "Error: Catch Tango Direccion",
                    Comprobante = "",
                    Fecha = DateTime.Now,
                    Error_Mensaje = error.Message,
                    Error_Severidad = "0",
                    Error_Estado = "0"
                };
                listaDeDatos.Add(spDireccionTango);
            }

            try
            {
                var listaNropedidos = crmContexto.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_SC_PedidoTango '{guid}';").AsEnumerable().ToList();
                listaDeDatos.AddRange(listaNropedidos);
            }
            catch (Exception error)
            {
                spPresupuestoTango = new SqlRespuesta
                {
                    Resultado = "Error: Catch Tango Pedidos",
                    Comprobante = "",
                    Fecha = DateTime.Now,
                    Error_Mensaje = error.Message,
                    Error_Severidad = "0",
                    Error_Estado = "0"
                };
                listaDeDatos.Add(spPresupuestoTango);

            }
               
            return listaDeDatos;

        }

        private SqlRespuesta EscribirClienteSP(Guid guid, CrmContexto crmContexto)
        {
            var spEscribeCliente = new SqlRespuesta();

            try
            {

                 spEscribeCliente = crmContexto.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_SC_CRM_CLIENTES '{guid}';").AsEnumerable().FirstOrDefault();

            }
            catch (Exception error)
            {
                spEscribeCliente = new SqlRespuesta { 
                Resultado = "Catch",
                Comprobante = "",
                Fecha = DateTime.Now,
                Error_Mensaje = error.Message,
                Error_Severidad = "0",
                Error_Estado = "0"
                };
                
            }

            return spEscribeCliente;

        }

        private SqlRespuesta EscribirPresupuestoSP(Guid guid, CrmContexto crmContexto)
        {
            var spEscribeCliente = new SqlRespuesta();

            try
            {

                spEscribeCliente = crmContexto.Set<SqlRespuesta>().FromSqlRaw($"EXECUTE dbo.SP_SC_CRM_PRESUPUESTO '{guid}';").AsEnumerable().FirstOrDefault();

            }
            catch (Exception error)
            {
                spEscribeCliente = new SqlRespuesta
                {
                    Resultado = "Catch",
                    Comprobante = "",
                    Fecha = DateTime.Now,
                    Error_Mensaje = error.Message,
                    Error_Severidad = "0",
                    Error_Estado = "0"
                };


            }

            return spEscribeCliente;

        }

        private IEnumerable<SqlRespuestaDomicilios> EscribirDomicilioSP(Guid guid, CrmContexto crmContexto)
        {

            List<SqlRespuestaDomicilios> ListadoDeDomicilios = new List<SqlRespuestaDomicilios>();

            var spEscribeCliente = new SqlRespuestaDomicilios();

            try
            {
                ListadoDeDomicilios = crmContexto.Set<SqlRespuestaDomicilios>().FromSqlRaw($"EXECUTE dbo.SP_SC_CRM_SUCURSALES '{guid}';").AsEnumerable().ToList();
            }
            catch (Exception error)
            {
                spEscribeCliente = new SqlRespuestaDomicilios
                {
                    Resultado = "Catch",
                    Comprobante = "",
                    Fecha = DateTime.Now,
                    Error_Mensaje = error.Message,
                    Error_Severidad = "0",
                    Error_Estado = "0",
                    NombreDomicilio = "Error"
                };

                ListadoDeDomicilios.Add(spEscribeCliente);

            }

            return ListadoDeDomicilios;

        }


        public async Task<IEnumerable<string>> validarCabecera(Presupuesto presupuesto)
        {
            ValidarPresupuesto validarcabecera = new ValidarPresupuesto();
            ValidationResult resultado = await validarcabecera.ValidateAsync(presupuesto);
            List<string> errores = new List<string>();

            if (!resultado.IsValid)
            {
                foreach (var error in resultado.Errors)
                {
                    string mensaje = "Falló la validación de Cabecera en: " + error.PropertyName + " - Error: " + error.ErrorMessage;
                    errores.Add(mensaje);
                }
            }

            return errores;
        }

        public async Task<IEnumerable<string>> validarCliente(Cliente cliente)
        {
            ValidarCliente validarcliente = new ValidarCliente();
            ValidationResult resultado = await validarcliente.ValidateAsync(cliente);
            List<string> errores = new List<string>();

            if (!resultado.IsValid)
            {
                foreach (var error in resultado.Errors)
                {
                    string mensaje = "Falló la validación de Cliente en: " + error.PropertyName + " - Error: " + error.ErrorMessage;
                    errores.Add(mensaje);
                }
            }

            return errores;
        }

        public async Task<IEnumerable<string>> validarDetalle(Detalle detalle)
        {

            ValidarDetalle validardetalle = new ValidarDetalle();
            ValidationResult resultado = await validardetalle.ValidateAsync(detalle);
            List<string> errores = new List<string>();

            if (!resultado.IsValid)
            {
                foreach (var error in resultado.Errors)
                {
                    string mensaje = "Falló la validación del Renglón en: " + error.PropertyName + " - Error: " + error.ErrorMessage;
                    errores.Add(mensaje);
                }
            }

            return errores;
        }

        public async Task<IEnumerable<string>> validarDomicDeEntrega(DireccionDeEntrega direcciones)
        {
            ValidarDireccionDeEntrega validarDireccion = new ValidarDireccionDeEntrega();
            List<string> errores = new List<string>();
            ValidationResult resultado = await validarDireccion.ValidateAsync(direcciones);

                if (!resultado.IsValid)
                {
                    foreach (var error in resultado.Errors)
                    {
                        string mensaje = "Falló la validación del Domicilio de Entrega en: " + error.PropertyName + " - Error: " + error.ErrorMessage;
                        errores.Add(mensaje);
                    }
                }

            return errores;
        }

        public async Task<IEnumerable<string>> validarTransaccion(Transaccion transaccion)
        {
            List<string> ListadoDeErrores = new List<string>();

            var erroresCliente = await validarCliente(transaccion.Cliente);
            ListadoDeErrores.AddRange(erroresCliente);

            var erroresCabecera = await validarCabecera(transaccion.Presupuesto);
            ListadoDeErrores.AddRange(erroresCabecera);

            foreach (Detalle item in transaccion.Detalles)
            {
                var erroresDetalle = await validarDetalle(item);
                ListadoDeErrores.AddRange(erroresDetalle);
            }

            foreach (DireccionDeEntrega item in transaccion.DireccionesDeEntrega)
            {
                var erroresDeDomicilio = await validarDomicDeEntrega(item);
                ListadoDeErrores.AddRange(erroresDeDomicilio);

            }

            return ListadoDeErrores;
        }

      
    }
}
