using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;

namespace Autorepuestos.Controllers
{
    //Líneas para controlar el borrado de sesión y que el usuario no se devuelva
    [ResponseCache(NoStore = true, Duration = 0)]
    [FiltroSesiones]
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuariosModel _usuariosModel;
        private readonly IErroresModel _ErroresModel;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuariosModel usuariosModel,
            IErroresModel erroresModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public ActionResult VerUsuarios(UsuariosEntities usuario)
        {
            try
            {
                return View(_usuariosModel.VerUsuarios(usuario));
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ModificarUsuarioAdmin(int id)
        {
            try
            {
                var resultado = _usuariosModel.ConsultarRol();

                if (resultado != null)
                {
                    var opcionesRol = new List<SelectListItem>();
                    foreach (var item in resultado.RespuestaUsuarios)
                        opcionesRol.Add(new SelectListItem { Text = item.NombreRol, Value = item.pIdRol.ToString() });


                    ViewBag.ComboRol = opcionesRol;

                    var consulta = _usuariosModel.VerUsuario(id);
                    if (consulta != null)
                        return View(consulta);
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
        public ActionResult ModificarUsuarioAdmin(UsuariosEntities usuario)
        {
            ModelState.Remove("IdUsuario");
            ModelState.Remove("pContrasena");
            if (ModelState.IsValid)
            {
                try
                {
                    _usuariosModel.ModificarUsuario(usuario);

                    //este llamara a la interfaz
                    TempData["MensajeModificacionUsuarioAdmin"] = true;
                    return RedirectToAction("ModificarUsuarioAdmin");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else //Con estas nuevas líneas se recarga el DropDown
            {
                var resultado = _usuariosModel.ConsultarRol();

                if (resultado != null)
                {
                    var opcionesRol = new List<SelectListItem>();
                    foreach (var item in resultado.RespuestaUsuarios)
                        opcionesRol.Add(new SelectListItem { Text = item.NombreRol, Value = item.pIdRol.ToString() });


                    ViewBag.ComboRol = opcionesRol;


                    return View(usuario);
                }
                else
                    return View("Error");
            }

        }
        [HttpGet]
        public ActionResult ModificarUsuarioUno(int id)
        {
            try
            {

                var consulta = _usuariosModel.VerUsuario(id);
                if (consulta != null)
                    return View(consulta);
                return View();

            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }


        [HttpPost]
        public ActionResult ModificarUsuarioUno(UsuariosEntities usuario)
        {
            ModelState.Remove("IdUsuario");
            if (usuario.pContrasena == null)
            {
                ModelState.Remove("pContrasena");

                if (ModelState.IsValid)
                {
                    try
                    {

                        _usuariosModel.ModificarUsuario(usuario);
                        if (usuario.pIdRol == 1)
                        {
                            TempData["MensajeModificacionUsuarioUnoCliente"] = true;
                            return RedirectToAction("ModificarUsuarioUno");
                        }
                        //este llamara a la interfaz
                        TempData["MensajeModificacionUsuarioUno"] = true;
                        return RedirectToAction("ModificarUsuarioUno");

                    }
                    catch (Exception ex)
                    {
                        RegistrarErrores(ex, ControllerContext);
                        return View("Error");
                    }
                }
                else
                {
                    return View();
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    try
                    {

                        _usuariosModel.ModificarUsuario(usuario);
                        if (usuario.pIdRol == 1)
                        {
                            TempData["MensajeModificacionUsuarioUnoCliente"] = true;
                            return RedirectToAction("ModificarUsuarioUno");
                        }
                        //este llamara a la interfaz
                        TempData["MensajeModificacionUsuarioUno"] = true;
                        return RedirectToAction("ModificarUsuarioUno");

                    }
                    catch (Exception ex)
                    {
                        RegistrarErrores(ex, ControllerContext);
                        return View("Error");
                    }
                }
                else
                {
                    return View();
                }
            }
        }

        [HttpGet]
        public ActionResult ModificarUsuarioTrabajador(int id)
        {

            try
            {

                var consulta = _usuariosModel.VerUsuario(id);
                if (consulta != null)
                    return View(consulta);
                return View();

            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }

        }

        [HttpPost]
        public ActionResult ModificarUsuarioTrabajador(UsuariosEntities usuario)
        {
            ModelState.Remove("IdUsuario");
            ModelState.Remove("pContrasena");
            if (ModelState.IsValid)
            {
                try
                {
                    _usuariosModel.ModificarUsuario(usuario);

                    //este llamara a la interfaz
                    TempData["MensajeModificacionUsuarioTrabajador"] = true;
                    return RedirectToAction("ModificarUsuarioTrabajador");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult CambiarEstadoUsuario(int id)
        {
            try
            {
                _usuariosModel.CambiarEstadoUsuario(id);
                return RedirectToAction("VerUsuarios", "Usuario");
            }
            catch (Exception ex)
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
