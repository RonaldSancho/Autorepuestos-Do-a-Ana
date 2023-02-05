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
        private readonly IPedidosModel _PedidosModel;

        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel, IPedidosModel pedidosModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
            _PedidosModel = pedidosModel;
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

        /*Pasar esto a otro controller*/
        [HttpGet]
        public ActionResult MostrarPedidoProveedores(PedidosEntities pedido)
        {
            return View(_PedidosModel.VerPedidos(pedido));
        }

        [HttpGet]
        public ActionResult AgregarPedidoProveedores()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgregarPedidoProveedores(PedidosEntities pedido)
        {
            _PedidosModel.Crear(pedido);
            return View("MostrarPedidoProveedores");
        }

        [HttpGet]
        public ActionResult EliminarPedido(int id)
        {
            _PedidosModel.eliminar(id);
            return RedirectToAction("MostrarPedidoProveedores");

        }


        [HttpGet]
        public ActionResult ModificarPedidoProveedores()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ModificarPedidoProveedores(PedidosEntities pedido)
        {
            //este llamara a la interfaz
            _PedidosModel.Editar(pedido);
            return RedirectToAction("MostrarPedidoProveedores");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}