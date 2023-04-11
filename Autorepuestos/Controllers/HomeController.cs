using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuariosModel _UsuariosModel;
        private readonly IPedidosModel _PedidosModel;
        private readonly ICatalogosModel _CatalogosModel;
        private readonly IEntregasModel _EntregasModel;
        private readonly IConfiguration _configuration;
        private readonly IErroresModel _ErroresModel;

        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel, IPedidosModel pedidosModel, 
            ICatalogosModel catalogoModel, IEntregasModel entregasModel, IConfiguration configuration, IErroresModel erroresModel)
        {
            _logger = logger;
            _UsuariosModel = usuariosModel;
            _PedidosModel = pedidosModel;
            _CatalogosModel = catalogoModel;
            _EntregasModel = entregasModel;
            _configuration = configuration;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario(UsuariosEntities usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _UsuariosModel.RegitrarUsuario(usuario);
                    TempData["MensajeUsuarioCreado"] = true;
                    return RedirectToAction("RegistrarUsuario", "Home");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("ErrorLogin");
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult CorreoExistente(string pcorreo)
        {
            return Json(_UsuariosModel.CorreoExistente(pcorreo));
        }

        [HttpGet]
        public IActionResult RecuperarContrasenna()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("ErrorLogin");
            }
        }

        [HttpPost]
        public IActionResult RecuperarContrasenna(string pcorreo)
        {
            try
            {
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("ErrorLogin");
            }
        }

        [HttpGet]
        public IActionResult Recuperar(UsuariosEntities usuario)
        {
            try
            {
                var resultado = _UsuariosModel.RecuperarContrasenna(usuario);

                string Email = _configuration.GetSection("EmailConfiguracion:Email").Value;
                string titulo = _configuration.GetSection("EmailConfiguracion:Titulo").Value;
                string Contraseña = _configuration.GetSection("EmailConfiguracion:Contraseña").Value;
                string Host = _configuration.GetSection("EmailConfiguracion:Host").Value;
                string Puerto = _configuration.GetSection("EmailConfiguracion:Puerto").Value;

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(Email));
                email.To.Add(MailboxAddress.Parse(resultado.pCorreo));
                email.Subject = titulo;
                email.Body = new TextPart(TextFormat.Html)
                { Text = "<h1>La contraseña del usuario " + resultado.pCorreo + " es " + resultado.pContrasena + "</h1>" };

                using var smtp = new SmtpClient();
                smtp.Connect(Host, int.Parse(Puerto), false);
                smtp.Authenticate(Email, Contraseña);
                smtp.Send(email);
                smtp.Disconnect(true);
            }
            catch
            {
                ViewBag.mensaje = "Correo no registrado";
                return View("Index");
            }
            return null;

        }

        [HttpGet]
        [FiltroSesiones]
        public IActionResult CerrarSesion() //el cerrar sesion no borra la variable de sesion
        {
            try
            {
                HttpContext.Session.Clear();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("ErrorLogin");
            }
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private void RegistrarErrores(Exception ex, ControllerContext contexto)
        {
            ErroresEntities errores = new ErroresEntities();
            errores.Origen = contexto.ActionDescriptor.ControllerName + "-" + contexto.ActionDescriptor.ActionName;
            errores.Mensaje = ex.Message;
            errores.IdUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
            _ErroresModel.RegistrarErrores(errores);
        }
    }
}