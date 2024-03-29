﻿using Autorepuestos.Entities;
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
            try
            {
                return View();
            } catch (Exception ex)
            {
                RegistrarBitacora(ex, ControllerContext);
                return View("Error");
            }
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
                    RegistrarBitacora(ex, ControllerContext);
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
                RegistrarBitacora(ex, ControllerContext);
                return View("ErrorLogin");
            }
        }

        [HttpPost]
        public IActionResult RecuperarContrasenna(string pcorreo)
        {
            try
            {
                TempData["ExitoMensaje"] = "Se ha enviado un mensaje a su correo electrónico. " +
                    "Verifique su bandeja de entrada para recuperar su contraseña.";

                return RedirectToAction("RecuperarContrasenna", "Home");
            }
            catch (Exception ex)
            {
                RegistrarBitacora(ex, ControllerContext);
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
                {
                    Text = "<h1>Estimado/a " + resultado.pCorreo + ",</h1>" +
                           "<p>Hemos recibido su solicitud de recuperación de contraseña para su cuenta en Autorepuestos Doña Ana.</p>" +
                           "<p>A continuación, le proporcionamos la información solicitada:</p>" +
                           "<p><strong>Correo electrónico:</strong> " + resultado.pCorreo + "</p>" +
                           "<p><strong>Contraseña:</strong> " + resultado.pContrasena + "</p>" +
                           "<p>Si tiene alguna pregunta o necesita asistencia adicional, no dude en contactarnos.</p>" +
                           "<p>Atentamente,</p>" +
                           "<p>El equipo de Autorepuestos Doña Ana</p>"
                };

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
        public IActionResult SobreNosotros()
        {
            return View();
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
            errores.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            _ErroresModel.RegistrarErrores(errores);
        }

        private void RegistrarBitacora(Exception ex, ControllerContext contexto)
        {
            ErroresEntities errores = new ErroresEntities();
            errores.Accion = contexto.ActionDescriptor.ControllerName + "-" + contexto.ActionDescriptor.ActionName;
            errores.Resultado = ex.Message;
            _ErroresModel.RegistrarBitacora(errores);
        }
    }
}