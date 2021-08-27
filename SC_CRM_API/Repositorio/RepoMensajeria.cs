using Microsoft.EntityFrameworkCore;
using SC_CRM_API.Contextos;
using SC_CRM_API.Entidades.BaseDeDatos;
using SC_CRM_API.Entidades.Dtos;
using SC_CRM_API.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
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

        public async Task<bool> confeccionarMail(MailDto parametros)
        {
            PresupuestoDeConsulta presupuesto = new PresupuestoDeConsulta();
            List<DetalleDeConsulta> renglones = new List<DetalleDeConsulta>();
            ClienteDeConsulta cliente = new ClienteDeConsulta();

            Sucursal contexto = await credencialesAsync(parametros.Sucursal);

            //--Levantar Data
            await using (var _crmDbContext = new CrmContexto(contexto))
            {
                presupuesto = await _crmDbContext.PresupuestosParaConsulta.Where(i => i.IdPresupuesto == parametros.Presupuesto).FirstOrDefaultAsync();
                renglones = await _crmDbContext.DetallesParaConsulta.Where(i => i.IdPresupuesto == parametros.Presupuesto).ToListAsync();
                cliente = await _crmDbContext.ClientesDeConsulta.Where(c => c.IdCliente == presupuesto.IdCliente).FirstOrDefaultAsync();
            }

            if (presupuesto == null || renglones.Count < 1)
                return false;


            //--Levantar Plantilla
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\Plantillas\\" + parametros.Plantilla + ".html";
            string plantilla = "";
            string constante = "<tr><th>**SKU**</th><th>**DESCRIPCION**</th><th>**CANTIDAD**</th><th>**IMPORTE**</th></tr>";
            string renglonesParaAgregar = string.Empty;

            if (File.Exists(path))
                plantilla = await File.ReadAllTextAsync(path);
            else
                return false;


            if (string.IsNullOrEmpty(plantilla))
                return false;


            //--Reemplazar en la plantilla
            string reemplazo_1 = plantilla.Replace("**CONTACTO**", cliente.RazonSocial);

            foreach (var renglon in renglones)
            {
                string agregar = constante.Replace("**SKU**", renglon.CodigoArticulo);
                string agregar_2 = agregar.Replace("**DESCRIPCION**", renglon.Descripcion);
                string agregar_3 = agregar_2.Replace("**CANTIDAD**", renglon.Cantidad.ToString());
                string agregar_4 = agregar_3.Replace("**IMPORTE**", renglon.Precio.ToString());
                renglonesParaAgregar += agregar_4;
            }

            string reemplazo_2 = reemplazo_1.Replace("**RENGLONES**", renglonesParaAgregar);
            string reemplazo_3 = reemplazo_2.Replace("**FIRMA**", "Guille el Loco");



            //-- Escribir el Mail
            Email mensaje = new Email();
            mensaje.Asunto = "Prueba de PRESUPUESTO";
            mensaje.De = 1100;
            mensaje.Para = "desarrollo@sommiercenter.com";
            mensaje.Cuerpo = reemplazo_3;
            mensaje.HTML = true;
            mensaje.TmeStamp = DateTime.Now;
            mensaje.Usuario = Environment.UserName;

            //var enviado = await guardarParaEnvio(mensaje);
            envioDirecto(mensaje);

            //if (enviado)
                return true;
            //else
              ///  return false;
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


        //--prueba delista blanca + salida directa
        public void envioDirecto(Email mensaje)
        {
            using (SmtpClient SmtpServer = new SmtpClient("smtp-relay.gmail.com"))
            { 

                using(MailMessage mail = new MailMessage())
                { 

                    mail.From = new MailAddress("Guille_El_Loco@sommiercenter.com");
                    mail.To.Add(mensaje.Para);
                    mail.Bcc.Add("gabriel.ceravolo@sommiercnter.com");
                    mail.CC.Add("emanuel.villalva@sommiercnter.com");
                    mail.CC.Add("juan.salazar@sommiercnter.com");
                    mail.Subject = mensaje.Asunto;
                    mail.Body = mensaje.Cuerpo;
                    mail.IsBodyHtml = true;

                    Attachment adjunto;
                    string ruta = AppDomain.CurrentDomain.BaseDirectory + "\\Plantillas\\presupuesto.pdf";

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
    }
}
