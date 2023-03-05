using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    //Evita que el usuario no se regrese, poner en todos los Controllers que se creen
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuariosModel _UsuariosModel;
        private readonly IPedidosModel _PedidosModel;
        private readonly ICatalogosModel _CatalogosModel;
        private readonly IEntregasModel _EntregasModel;

        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel, IPedidosModel pedidosModel, 
            ICatalogosModel catalogoModel, IEntregasModel entregasModel)
        {
            _logger = logger;
            _UsuariosModel = usuariosModel;
            _PedidosModel = pedidosModel;
            _CatalogosModel = catalogoModel;
            _EntregasModel = entregasModel;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegistrarUsuario(UsuariosEntities usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _UsuariosModel.RegitrarUsuario(usuario);
                    return View("Index");
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
        public ActionResult CorreoExistente(string pcorreo)
        {
            return Json(_UsuariosModel.CorreoExistente(pcorreo));
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}