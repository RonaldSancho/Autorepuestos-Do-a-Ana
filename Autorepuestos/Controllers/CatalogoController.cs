using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    [ResponseCache (NoStore =true, Duration = 0)]
    public class CatalogoController : Controller
    {

        private readonly ILogger<CatalogoController> _logger;
        private readonly ICatalogosModel _CatalogosModel;
        private readonly IUsuariosModel _UsuariosModel;

        public CatalogoController(ILogger<CatalogoController> logger, ICatalogosModel catalogoModel, IUsuariosModel usuariosModel)
        {
            _logger = logger;
            _CatalogosModel = catalogoModel;
            _UsuariosModel = usuariosModel;
        }

        [HttpPost]
        public IActionResult VerCatalogos(CatalogosEntities catalogo, UsuariosEntities usuarios)
        {
            try
            {
                var resultado = _UsuariosModel.ValidarUsuarios(usuarios);
                if (resultado != null)
                {
                    HttpContext.Session.SetInt32("IdUsuario", resultado.IdUsuario);
                    HttpContext.Session.SetInt32("IdRol", resultado.pIdRol);
                    return RedirectToAction("VerCatalogos", "Catalogo");
                }
                else
                {
                    TempData["MensajeCredenciales"] = true;
                    return RedirectToAction("Index", "Home");
                }
            }
            catch(Exception ex)
            {
                ViewBag.mensaje = "Se presentó un inconveniente con el sistema.";
                return RedirectToAction("Index","Home");
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
            
                var consulta = _CatalogosModel.VerProductoCatalogo(id);
                if (consulta != null)
                    return View(consulta);
                return View();
            
        }

        

    }
}
