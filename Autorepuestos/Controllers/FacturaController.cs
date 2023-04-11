using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Configuration;

namespace Autorepuestos.Controllers
{
    public class FacturaController : Controller
    {

        private readonly IFacturaModel _facturaModel;
        private readonly ILogger<FacturaController> _logger;
        private readonly IConfiguration _configuration;
        public FacturaController(IFacturaModel facturaModel, ILogger<FacturaController> logger, IConfiguration configuration)
        {
            _facturaModel = facturaModel;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult VerFacturas()
        {
            return View(_facturaModel.VerFacturas());
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
            _facturaModel.EliminarFactura(id);
            return RedirectToAction("VerFacturas", "Factura");
        }

        [HttpGet]
        public IActionResult VerDetalleFactura(int id)
        {
            var resultado = _facturaModel.VerDetalleFactura(id);
            if (resultado != null)
                return View(resultado);
            else
                return View("Error");
        }

        [HttpGet]
        public IActionResult ConsultarTipoPago()
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
            "<h3>Número de Factura:<b style=\"color:#02C7FC\">"+ resultado.IdFactura + "</b> </h3><br>\r\n" +
            "<h3>Fecha de Compra:" + resultado.Fecha + " </h3><br>\r\n " +
            "<h3>Monto Total:" + resultado.MontoTotal + " </h3><br>\r\n   " +
            " <footer style=\"text-align: center;\">Recuerde que si desea descargar la factura, dirijase a la pagina en la sección de facturas y seleccione la factura deseada </footer>" };

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
            _facturaModel.CambiarEstadoFactura(id);
            return RedirectToAction("VerFacturas", "Factura");
        }

    }
}
