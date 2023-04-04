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

        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel, IPedidosModel pedidosModel, 
            ICatalogosModel catalogoModel, IEntregasModel entregasModel, IConfiguration configuration)
        {
            _logger = logger;
            _UsuariosModel = usuariosModel;
            _PedidosModel = pedidosModel;
            _CatalogosModel = catalogoModel;
            _EntregasModel = entregasModel;
            _configuration = configuration;
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
                    ViewBag.mensaje = "Se presentó un inconveniente con el sistema.";
                    return View();
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
            catch
            {
                ViewBag.mensaje = "Correo no registrado";
                return View("Index");
            }
        }

        [HttpPost]
        public IActionResult RecuperarContrasenna(string pcorreo)
        {
            try
            {
                return View("Index");
            }
            catch
            {
                ViewBag.mensaje = "Correo no registrado";
                return View("Index");
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
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}