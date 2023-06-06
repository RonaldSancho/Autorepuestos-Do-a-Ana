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
    public class CategoriaController : Controller
    {
        private readonly ILogger<CategoriaController> _logger;
        private readonly ICategoriaModel _CategoriaModel;
        private readonly IErroresModel _ErroresModel;


        public CategoriaController(ILogger<CategoriaController> logger, ICategoriaModel categoriaModel, IErroresModel erroresModel)
        {
            _logger = logger;
            _CategoriaModel = categoriaModel;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public ActionResult MostrarCategoria(CategoriaEntities categoria)
        {
            try
            {
                return View(_CategoriaModel.VerCategorias(categoria));
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AgregarCategoria()
        {
            try
            {

                return View();


            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AgregarCategoria(CategoriaEntities categoria)
        {
            ModelState.Remove("IdCategoria");

            if (ModelState.IsValid)
            {
                try
                {
                    _CategoriaModel.Crear(categoria);
                    TempData["MensajeCategoriaAgregado"] = true;
                    return RedirectToAction("AgregarCategoria");
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
        public ActionResult ModificarCategoria(int id)
        {
            try
            {

                var consulta = _CategoriaModel.VerCategoria(id);
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
        public ActionResult ModificarCategoria(CategoriaEntities categoria)
        {
            ModelState.Remove("pIdCategoria");
            if (ModelState.IsValid)
            {
                try
                {
                    //este llamara a la interfaz
                    _CategoriaModel.Editar(categoria);
                    TempData["MensajeModificacionCategoria"] = true;
                    return RedirectToAction("ModificarCategoria");
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
        public ActionResult DetalleCategoria(int id)
        {
            try
            {
                var resultado = _CategoriaModel.VerCategoria(id);
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
