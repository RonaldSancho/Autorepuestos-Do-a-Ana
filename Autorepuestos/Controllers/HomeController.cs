using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuariosModel _usuariosModel;

        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConsultarProductos(UsuariosEntities usuario)
        {
            if (_usuariosModel.ValidarUsuario(usuario) != null)
                return View();
            else
                return View("Index");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}