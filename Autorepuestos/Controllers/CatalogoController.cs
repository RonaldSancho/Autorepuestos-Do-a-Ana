using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class CatalogoController : Controller
    {

        private readonly ILogger<CatalogoController> _logger;
        private readonly ICatalogosModel _CatalogosModel;
        private readonly IUsuariosModel _UsuariosModel;
        private readonly IErroresModel _ErroresModel;

        public CatalogoController(ILogger<CatalogoController> logger, ICatalogosModel catalogoModel,
            IUsuariosModel usuariosModel, IErroresModel erroresModel)
        {
            _logger = logger;
            _CatalogosModel = catalogoModel;
            _UsuariosModel = usuariosModel;
            _ErroresModel = erroresModel;
        }

        [HttpPost]
        public IActionResult VerCatalogos(CatalogosEntities catalogo, UsuariosEntities usuarios)
        {
            try
            {
                if (string.IsNullOrEmpty(usuarios.pCorreo) || string.IsNullOrEmpty(usuarios.pContrasena))
                {
                    TempData["MensajeCredenciales"] = true;
                    return RedirectToAction("Index", "Home");
                }

                var resultado = _UsuariosModel.ValidarUsuarios(usuarios);

                if (resultado != null)
                {
                    HttpContext.Session.SetInt32("IdUsuario", resultado.IdUsuario);
                    HttpContext.Session.SetInt32("IdRol", resultado.pIdRol);
                    return RedirectToAction("VerCatalogos", "Catalogo");
                }
                else
                {
                    if (!usuarios.Estado)
                    {
                        TempData["MensajeCredenciales2"] = true;
                    }
                    else
                    {
                        TempData["MensajeCredenciales"] = true;
                    }

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                RegistrarBitacora(ex, ControllerContext);
                return View("Error");
            }
        }


        [HttpGet]
        [FiltroSesiones]
        public IActionResult VerCatalogos(CatalogosEntities catalogo)
        {
            return View(_CatalogosModel.VerCatalogos(catalogo));
        }

        [HttpGet]
        public ActionResult VerProductoCatalogo(int id)
        {
            try
            {
                var consulta = _CatalogosModel.VerProductoCatalogo(id);
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
