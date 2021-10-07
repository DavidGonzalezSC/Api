using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;

namespace SC_CRM_API.Repositorio
{
    public class RepoMensajeria : IMensajeria
    {

        private readonly IServiciosSucursales _sucursales;
        private MensajeriaDbContext _contexto;

        public RepoMensajeria(MensajeriaDbContext contexto, IServiciosSucursales sucursales)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(MensajeriaDbContext));
            _sucursales = sucursales ?? throw new ArgumentNullException(nameof(IServiciosSucursales));
        }


        public async Task<Sucursal> credencialesAsync(string sucursal)
        {
            Sucursal credenciales = await _sucursales.Sucursal(sucursal);

            if (credenciales != null) //forzar parametros de prueba
                return credenciales;
            else
                return null;

        }

        public async Task<string> confeccionarPdf(MailDto parametros)
        {


            Sucursal contexto = await credencialesAsync(parametros.Sucursal);
            if (contexto == null)
                return "";

            PresupuestoDevueltoDbDto presupuestoAImprimir = new PresupuestoDevueltoDbDto();
            presupuestoAImprimir.NumeroPedido = parametros.Presupuesto;
            presupuestoAImprimir.Sucursal = parametros.Sucursal;

            //--Levantar Data
            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                presupuestoAImprimir.Presupuesto = await _crmDbContext.PresupuestosParaConsulta.Where(i => i.IdPresupuesto == parametros.Presupuesto).FirstOrDefaultAsync();
                presupuestoAImprimir.DetallesDto = await _crmDbContext.DetallesParaConsultaVista.Where(i => i.IdPresupuesto == parametros.Presupuesto).ToListAsync();
                presupuestoAImprimir.Cliente = await _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == presupuestoAImprimir.Presupuesto.IdCliente).FirstOrDefaultAsync();
            }

            if (presupuestoAImprimir.Presupuesto == null || presupuestoAImprimir.DetallesDto.Count < 1)
                return "";


            //escribir el archivo json con la data
            string nombreArchivo = presupuestoAImprimir.Sucursal + "_" + presupuestoAImprimir.Identificador + ".json";
            string pati_json = AppDomain.CurrentDomain.BaseDirectory + $"\\Comandos\\Json\\{nombreArchivo}";
            using FileStream crearStream = File.Create(pati_json);
            await JsonSerializer.SerializeAsync(crearStream, presupuestoAImprimir);
            crearStream.Close();

            EjecutarCreacionPDF(nombreArchivo);

            //if (enviado)
            string resultado = Path.GetFileNameWithoutExtension(nombreArchivo);
            return resultado;
            //else
              ///  return false;
        }

        public async Task<Email> crear_cuerpoAsync(PresupuestoDevueltoDbDto transacc, string comentarios)
        {
            //--Levantar Plantilla
            string codsucursal = transacc.Sucursal;
            if (codsucursal == "TP")
                codsucursal = "TH";

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Plantillas\\presupuesto.html";
            string plantilla = "";
            string constante = "<tr><td style=\"text-align:right; border-bottom: 1pt solid black;\">**CANTIDAD**</td><td style=\"text-align:left; border-bottom: 1pt solid black;\">**DESCRIPCION**</td><td style=\"text-align:right; border-bottom: 1pt solid black;\">**IMPORTESINDESC**</td><th style=\"text-align:right; border-bottom: 1pt solid black;\">**IMPORTE**</th></tr>";
            string renglonesParaAgregar = string.Empty;

            if (File.Exists(path))
                plantilla = await File.ReadAllTextAsync(path);
            else
                return null;


            if (string.IsNullOrEmpty(plantilla))
                return null;


            //--Reemplazar en la plantilla
            string reemplazo_0 = plantilla.Replace("**FECHA**", transacc.Presupuesto.FechaDeCotizacion.ToLongDateString());
            string reemplazo_1 = reemplazo_0.Replace("**CONTACTO**", transacc.Cliente.RazonSocial);
            decimal totalGeneral = 0;
            foreach (var renglon in transacc.DetallesDto)
            {
                var importeCondesc = renglon.Precio * (1 - renglon.Bonif / 100) * renglon.Cantidad;
                string agregar_1 = constante.Replace("**IMPORTESINDESC**", Math.Round(renglon.Precio, 2).ToString("0,0.00"));
                string agregar_2 = agregar_1.Replace("**DESCRIPCION**", $"{renglon.Descripcion}({renglon.CodigoArticulo})");
                string agregar_3 = agregar_2.Replace("**CANTIDAD**", Math.Truncate(renglon.Cantidad).ToString());
                string agregar_4 = agregar_3.Replace("**IMPORTE**", Math.Round(importeCondesc, 2).ToString("0,0.00"));
                totalGeneral = totalGeneral  + Math.Round(renglon.Precio, 2);
                renglonesParaAgregar += agregar_4;
            }

            string reemplazo_2 = reemplazo_1.Replace("**IMPORTETOTAL**", Math.Round(totalGeneral, 2).ToString("0,0.00"));
            string reemplazo_3 = reemplazo_2.Replace("**RENGLONES**", renglonesParaAgregar);
            string reemplazo_4 = reemplazo_3.Replace("**COMENTARIOS**", comentarios);
            string reemplazo_5 = reemplazo_4.Replace("**FECHACADUCA**", transacc.Presupuesto.FechaDeCotizacion.AddDays(1).ToLongDateString());
            string reemplazo_6 = reemplazo_5.Replace("**FIRMA**", ""); //AGREGAR AL VENDEDOR
            string reemplazo_7 = reemplazo_6.Replace("**FOOTER**", $"https://sommiercenter.com/media/firmas/{codsucursal}.jpg");
            



            //-- Escribir el Mail
            Email mensaje = new Email();
            mensaje.Asunto = "PRESUPUESTO SOMMIERCENTER";
            mensaje.De = 1100;
            mensaje.Para = transacc.Cliente.Email.ToLower().Trim();
            mensaje.CCO = "desarrollo@sommiercenter.com";
            mensaje.Cuerpo = reemplazo_7;
            mensaje.HTML = true;
            mensaje.TmeStamp = DateTime.Now;
            mensaje.Usuario = Environment.UserName;

            //enviar mail

            return mensaje;

        }

        public async Task<bool> guardarParaEnvio(Email mensaje)
        {
            _contexto.MailsAEnviar.Add(mensaje);
            var guardo = await _contexto.SaveChangesAsync();

            if (guardo > 0)
                return true;
            else
                return false;
        }

        public IEnumerable<string> ListarPlantillas()
        {
            List<string> listado = new List<string>();

            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Plantillas";

            if (!Directory.Exists(path))
            {
                listado.Add("VACIO");

            }
            else
            {
                var archivos = Directory.EnumerateFiles(path).Select(Path.GetFileName);
                listado.AddRange(archivos);
            }

            return listado;

        }


        //--Ejecutar un comando??
        static void EjecutarCreacionPDF(string parametro)
        {
            
            ProcessStartInfo processInfo;
            Process proceso;

            //processInfo = new ProcessStartInfo("cmd.exe", "/c " + parametro);
            //string cadenaEjecutoria = AppDomain.CurrentDomain.BaseDirectory + $"\\Comandos\\GenerarPdf.exe {parametro}";
            string cadenaEjecutoria = AppDomain.CurrentDomain.BaseDirectory + $"\\Comandos\\GenerarPdf.exe";
            processInfo = new ProcessStartInfo(cadenaEjecutoria, parametro);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;

            //-- Redireccionar la salida estandard por ahora..luego a un log o algo
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

            proceso = Process.Start(processInfo);

            //-- Capturar la Salidas
            proceso.OutputDataReceived += (object sender, DataReceivedEventArgs e) => Console.WriteLine("Salida ->" + e.Data);
            proceso.BeginOutputReadLine();

            proceso.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>  Console.WriteLine("Errores->" + e.Data);
            proceso.BeginErrorReadLine();

            proceso.WaitForExit();

            Console.WriteLine("CodSalida: {0}", proceso.ExitCode);
            proceso.Close();

        }

        //--prueba delista blanca + salida directa
        public void envioDirecto(Email mensaje, string archivo)
        {
            using (SmtpClient SmtpServer = new SmtpClient("smtp-relay.gmail.com"))
            { 

                using(MailMessage mail = new MailMessage())
                { 

                    mail.From = new MailAddress("Pruebas_Desarrollo@sommiercenter.com");
                    mail.To.Add(mensaje.Para);
                    mail.Bcc.Add("gabriel.ceravolo@sommiercnter.com");
                    mail.CC.Add("emanuel.villalva@sommiercnter.com");
                    mail.CC.Add("juan.salazar@sommiercnter.com");
                    mail.Subject = mensaje.Asunto;
                    mail.Body = mensaje.Cuerpo;
                    mail.IsBodyHtml = true;

                    Attachment adjunto;
                    //string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\Plantillas\\presupuesto.pdf";
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + $"\\Comandos\\Pdfs\\{archivo}.pdf";

                    if (ruta != null)
                    {
                        adjunto = new Attachment(ruta, MediaTypeNames.Application.Octet);
                        ContentDisposition disposition = adjunto.ContentDisposition;
                        disposition.CreationDate = File.GetCreationTime(ruta);
                        disposition.ModificationDate = File.GetLastWriteTime(ruta);
                        disposition.ReadDate = File.GetLastAccessTime(ruta);
                        disposition.FileName = Path.GetFileName(ruta);
                        disposition.Size = new FileInfo(ruta).Length;
                        disposition.DispositionType = DispositionTypeNames.Attachment;
                        mail.Attachments.Add(adjunto);
                    }

                    SmtpServer.Port = 587;
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);

                } 
            }
        }

        public async Task<bool> enviarMail(MailDto presupuesto)
        {
            var encontrarDatos = await ObtenerPresupuesto(presupuesto);

            if (string.IsNullOrEmpty(presupuesto.Observaciones))
                presupuesto.Observaciones = "";

            Email mensjeParaEnvio = await crear_cuerpoAsync(encontrarDatos, presupuesto.Observaciones);

            if (mensjeParaEnvio != null)
                return await guardarParaEnvio(mensjeParaEnvio);
            else
                return false;
        }

        private async Task<PresupuestoDevueltoDbDto> ObtenerPresupuesto(MailDto datos)
        {
            Sucursal contexto = await credencialesAsync(datos.Sucursal);
            if (contexto == null)
                return null;

            PresupuestoDevueltoDbDto presupuestoAImprimir = new PresupuestoDevueltoDbDto();
            presupuestoAImprimir.NumeroPedido = datos.Presupuesto;
            presupuestoAImprimir.Sucursal = datos.Sucursal;

            //--Levantar Data
            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                presupuestoAImprimir.Presupuesto = await _crmDbContext.PresupuestosParaConsulta.Where(i => i.IdPresupuesto == datos.Presupuesto).FirstOrDefaultAsync();
                presupuestoAImprimir.DetallesDto = await _crmDbContext.DetallesParaConsultaVista.Where(i => i.IdPresupuesto == datos.Presupuesto).ToListAsync();
                presupuestoAImprimir.Cliente = await _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == presupuestoAImprimir.Presupuesto.IdCliente).FirstOrDefaultAsync();
            }

            if (presupuestoAImprimir.Presupuesto == null || presupuestoAImprimir.DetallesDto.Count < 1)
                return null;
            else
                return presupuestoAImprimir;
        }
    }
}
