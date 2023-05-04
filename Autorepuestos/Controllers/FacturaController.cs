using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Configuration;
using Rotativa.AspNetCore;

namespace Autorepuestos.Controllers
{
    public class FacturaController : Controller
    {

        private readonly IFacturaModel _facturaModel;
        private readonly ILogger<FacturaController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IErroresModel _ErroresModel;
        public FacturaController(IFacturaModel facturaModel, ILogger<FacturaController> logger, 
            IConfiguration configuration, IErroresModel erroresModel)
        {
            _facturaModel = facturaModel;
            _logger = logger;
            _configuration = configuration;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public IActionResult VerFacturas()
        {
            try
            {
                return View(_facturaModel.VerFacturas());
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        //[HttpGet]
        //public IActionResult CrearFactura()
        //{
        //    var usuario = HttpContext.Session.GetInt32("IdUsuario");
        //    _facturaModel.CrearFactura(usuario);
        //    return RedirectToAction("VerCatalogos", "Catalogo");
        //}

        [HttpGet]
        public IActionResult EliminarFactura(int id) 
        {
            try
            {
                _facturaModel.EliminarFactura(id);
                return RedirectToAction("VerFacturas", "Factura");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult VerDetalleFactura(int id)
        {
            try
            {
                HttpContext.Session.SetInt32("IdFactura", id);
                var resultado = _facturaModel.VerDetalleFactura(id);
                if (resultado != null)
                    return View(resultado);
                else
                    return View("Error");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ConsultarTipoPago()
        {
            try
            {
                var result = _facturaModel.ConsultarTipoPago();
                var result2 = _facturaModel.ConsultarTipoRetiro();
                if (result != null && result2 != null)
                {
                    var dropdownTipoPago = new List<SelectListItem>();
                    var dropdownTipoRetiro = new List<SelectListItem>();
                    foreach (var item in result.RespuestaFacturas)
                        dropdownTipoPago.Add(new SelectListItem { Text = item.TipoPago, Value = item.IdTipoPago.ToString() });
                    foreach (var item in result2.RespuestaFacturas)
                        dropdownTipoRetiro.Add(new SelectListItem { Text = item.TipoRetiro, Value = item.IdTipoRetiro.ToString() });

                    ViewBag.ComboTipoPago = dropdownTipoPago;
                    ViewBag.ComboTipoRetiro = dropdownTipoRetiro;
                    return View();
                }
                else
                    return View("Error");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult ConsultarTipoPago(FacturaEntities entidad)
        {
            var usuario = HttpContext.Session.GetInt32("IdUsuario");
            _facturaModel.CrearFactura(entidad, usuario);

            entidad.IdUsuario = Convert.ToInt32(usuario);
            var resultado = _facturaModel.EnviarCorreoConfirmacion(entidad);

            string Email = _configuration.GetSection("EmailConfiguracion:Email").Value;
            string titulo = _configuration.GetSection("EmailConfiguracion:Titulo2").Value;
            string Contraseña = _configuration.GetSection("EmailConfiguracion:Contraseña").Value;
            string Host = _configuration.GetSection("EmailConfiguracion:Host").Value;
            string Puerto = _configuration.GetSection("EmailConfiguracion:Puerto").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Email));
            email.To.Add(MailboxAddress.Parse(resultado.Correo));
            email.Subject = titulo;
            email.Body = new TextPart(TextFormat.Html)
            { Text = "<h1 style=\"text-align: center;\">Tú compra ha sido exitosa</h1><br>\r\n    " +
            "<h3>Número de Factura :<b style=\"color:#02C7FC\">"+ resultado.IdFactura + "</b> .</h3><br>\r\n" +
            "<h3>Fecha de Compra :" + resultado.Fecha + " .</h3><br>\r\n " +
            "<h3>Detalle de la factura :" + resultado.Detalle + " .</h3><br>\r\n   " +
            "<h3>Monto Total :" + resultado.MontoTotal + " .</h3><br>\r\n   " +
            " <footer style=\"text-align: center;\">Recuerde que si desea descargar la factura, diríjase a la página en la sección de facturas y seleccione la factura que desea descargar.</footer>"
            };

            using var smtp = new SmtpClient();
            smtp.Connect(Host, int.Parse(Puerto), false);
            smtp.Authenticate(Email, Contraseña);
            smtp.Send(email);
            smtp.Disconnect(true);
            return RedirectToAction("VerCatalogos", "Catalogo");

        }

        [HttpGet]
        public IActionResult CambiarEstadoFactura(int id)
        {
            try
            {
                _facturaModel.CambiarEstadoFactura(id);
                return RedirectToAction("VerFacturas", "Factura");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult FacturasDelCliente()
        {
            try
            {
                var IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
                return View(_facturaModel.FacturasDelCliente(IdUsuario));
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        public IActionResult DescargarDetalle(int IdFactura)
        {
            try
            {
                var result= _facturaModel.VerDetalleFactura(IdFactura);
                return new ViewAsPdf("DescargarDetalle", result)
                {
                    FileName = $"Factura # {result.IdFactura}.pdf",
                    PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                    PageSize= Rotativa.AspNetCore.Options.Size.A4
                };
            }catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        private void RegistrarErrores(Exception ex, ControllerContext contexto)
        {
            ErroresEntities errores = new ErroresEntities();
            errores.Origen = contexto.ActionDescriptor.ControllerName + "-" + contexto.ActionDescriptor.ActionName;
            errores.Mensaje = ex.Message;
            errores.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            _ErroresModel.RegistrarErrores(errores);
        }
    }
}
