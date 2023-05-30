using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autorepuestos.Controllers
{
    //Líneas para controlar el borrado de sesión y que el usuario no se devuelva
    [ResponseCache(NoStore = true, Duration = 0)]
    [FiltroSesiones]
    public class EntregaController : Controller
    {
        private readonly ILogger<EntregaController> _logger;
        private readonly IEntregasModel _EntregasModel;
        private readonly IErroresModel _ErroresModel;

        public EntregaController(ILogger<EntregaController> logger, IEntregasModel entregasModel, 
            IErroresModel erroresModel)
        {
            _logger = logger;
            _EntregasModel = entregasModel;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public ActionResult VerEntregas(EntregasEntities entrega)
        {
            return View(_EntregasModel.VerEntregas(entrega));
        }

        [HttpGet]
        public ActionResult AgregarEntrega()
        {
            try
            {
                //var result1 = _EntregasModel.ConsultaEntregaProducto();
                var result2 = _EntregasModel.ConsultaEntregaUsuario();
                if (/*result1 != null &&*/ result2 != null)
                {
                    var dropdownProductos = new List<SelectListItem>();
                    var dropdownUsuarios = new List<SelectListItem>();

                    //foreach (var item in result1.RespuestaEntregas)
                    //    dropdownProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                    foreach (var item in result2.RespuestaEntregas)
                        dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboProducto = dropdownProductos;
                    ViewBag.ComboUsuario = dropdownUsuarios;
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
        public ActionResult AgregarEntrega(EntregasEntities entrega)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _EntregasModel.AgregarEntrega(entrega);
                    TempData["MensajeEntregaAgregada"] = true;
                    return RedirectToAction("AgregarEntrega", "Entrega");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else
            {
                //var result1 = _EntregasModel.ConsultaEntregaProducto();
                var result2 = _EntregasModel.ConsultaEntregaUsuario();
                if (/*result1 != null &&*/ result2 != null)
                {
                    var dropdownProductos = new List<SelectListItem>();
                    var dropdownUsuarios = new List<SelectListItem>();

                    //foreach (var item in result1.RespuestaEntregas)
                    //    dropdownProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                    foreach (var item in result2.RespuestaEntregas)
                        dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboProducto = dropdownProductos;
                    ViewBag.ComboUsuario = dropdownUsuarios;
                    return View();
                }
                else
                    return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EditarEntrega(int id)
        {
            try
            {
                var result2 = _EntregasModel.ConsultaEntregaUsuario();
                if (result2 != null)
                {
                    var dropdownEstado = new List<SelectListItem>();

                    dropdownEstado.Add(new SelectListItem { Text = "Pendiente", Value = "Pendiente" });
                    dropdownEstado.Add(new SelectListItem { Text = "Entregado", Value = "Entregado" });

                    var dropdownUsuarios = new List<SelectListItem>();
                    foreach (var item in result2.RespuestaEntregas)
                        dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboEstado = dropdownEstado;
                    ViewBag.ComboUsuario = dropdownUsuarios;

                    var consulta = _EntregasModel.ConsultarEntrega(id);
                    if (consulta != null)
                    {
                        return View(consulta);
                    }
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
        public ActionResult EditarEntrega(EntregasEntities entrega)
        {
            ModelState.Remove("pIdEntrega");

            if (ModelState.IsValid)
            {
                try
                {
                    _EntregasModel.Editar(entrega);
                    TempData["MensajeModificacionEntrega"] = true;
                    return RedirectToAction("EditarEntrega", "Entrega");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else
            {
                var result2 = _EntregasModel.ConsultaEntregaUsuario();
                if (result2 != null)
                {
                    var dropdownEstado = new List<SelectListItem>();

                    dropdownEstado.Add(new SelectListItem { Text = "Pendiente", Value = "Pendiente" });
                    dropdownEstado.Add(new SelectListItem { Text = "Entregado", Value = "Entregado" });

                    var dropdownUsuarios = new List<SelectListItem>();
                    foreach (var item in result2.RespuestaEntregas)
                        dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboEstado = dropdownEstado;
                    ViewBag.ComboUsuario = dropdownUsuarios;

                    return View(entrega);
                }
                else
                    return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EliminarEntrega(int id)
        {
            try
            {
                _EntregasModel.EliminarEntrega(id);
                return RedirectToAction("VerEntregas", "Entrega");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }

        }

        [HttpGet]
        public ActionResult EditarEntregaRepartidor(int id)
        {
            try
            {
                var result2 = _EntregasModel.ConsultaEntregaUsuario();
                if (result2 != null)
                {
                    var dropdownEstado = new List<SelectListItem>();

                    dropdownEstado.Add(new SelectListItem { Text = "Pendiente", Value = "Pendiente" });
                    dropdownEstado.Add(new SelectListItem { Text = "Entregado", Value = "Entregado" });

                    var dropdownUsuarios = new List<SelectListItem>();
                    foreach (var item in result2.RespuestaEntregas)
                        dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboEstado = dropdownEstado;
                    ViewBag.ComboUsuario = dropdownUsuarios;

                    var consulta = _EntregasModel.ConsultarEntrega(id);
                    if (consulta != null)
                    {
                        return View(consulta);
                    }
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
        public ActionResult EditarEntregaRepartidor(EntregasEntities entrega)
        {
            try
            {
                _EntregasModel.EditarEntregaRepartidor(entrega);
                return RedirectToAction("VerEntregas", "Entrega");
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
