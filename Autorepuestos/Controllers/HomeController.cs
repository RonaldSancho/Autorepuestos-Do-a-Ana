using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuariosModel _UsuariosModel;
        private readonly IPedidosModel _PedidosModel;
        private readonly ICatalogosModel _CatalogosModel;

        public HomeController(ILogger<HomeController> logger, IUsuariosModel usuariosModel, IPedidosModel pedidosModel, ICatalogosModel catalogoModel)
        {
            _logger = logger;
            _UsuariosModel = usuariosModel;
            _PedidosModel = pedidosModel;
            _CatalogosModel = catalogoModel;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConsultarProductos(UsuariosEntities usuario)
        {

            if (_UsuariosModel.ValidarUsuarios(usuario) != null)
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

        public ActionResult AgregarPedidoProveedores()
        {
            var resultado1 = _PedidosModel.ConsultaPedidoProducto();
            var resultado2 = _PedidosModel.ConsultaPedidoProveedor();
            var resultado3 = _PedidosModel.ConsultaPedidoUsuario();
            if (resultado1 != null && resultado2 != null && resultado3 != null)
            {
                var opcionesProductos = new List<SelectListItem>();
                var opcionesProveedores = new List<SelectListItem>();
                var opcionesUsuarios = new List<SelectListItem>();

                foreach (var item in resultado1.RespuestaPedidos)
                    opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                foreach (var item in resultado2.RespuestaPedidos)
                    opcionesProveedores.Add(new SelectListItem { Text = item.NombreProveedor, Value = item.IdProveedor.ToString() });

                foreach (var item in resultado3.RespuestaPedidos)
                    opcionesUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                ViewBag.ComboProductos = opcionesProductos;
                ViewBag.ComboProveedores = opcionesProveedores;
                ViewBag.ComboUsuarios = opcionesUsuarios;
                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult AgregarPedidoProveedores(PedidosEntities pedido)
        {

            _PedidosModel.Crear(pedido);
            return RedirectToAction("MostrarPedidoProveedores");
        }

        [HttpGet]
        public ActionResult EliminarPedido(int id)
        {
            _PedidosModel.eliminar(id);
            return RedirectToAction("MostrarPedidoProveedores");

        }


        [HttpGet]
        public ActionResult ModificarPedidoProveedores(int id)
        {
            var resultado1 = _PedidosModel.ConsultaPedidoProducto();
            var resultado2 = _PedidosModel.ConsultaPedidoProveedor();
            var resultado3 = _PedidosModel.ConsultaPedidoUsuario();
            if (resultado1 != null && resultado2 != null && resultado3 != null)
            {
                var opcionesProductos = new List<SelectListItem>();
                var opcionesProveedores = new List<SelectListItem>();
                var opcionesUsuarios = new List<SelectListItem>();

                foreach (var item in resultado1.RespuestaPedidos)
                    opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                foreach (var item in resultado2.RespuestaPedidos)
                    opcionesProveedores.Add(new SelectListItem { Text = item.NombreProveedor, Value = item.IdProveedor.ToString() });

                foreach (var item in resultado3.RespuestaPedidos)
                    opcionesUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                ViewBag.ComboProductos = opcionesProductos;
                ViewBag.ComboProveedores = opcionesProveedores;
                ViewBag.ComboUsuarios = opcionesUsuarios;

                var consulta = _PedidosModel.verPedido(id);
                if (consulta != null)
                    return View(consulta);
                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult ModificarPedidoProveedores(PedidosEntities pedido)
        {
            //este llamara a la interfaz
            _PedidosModel.Editar(pedido);
            return RedirectToAction("MostrarPedidoProveedores");

        }

        [HttpGet]
        public ActionResult DetallePedido(int id)
        {
            var resultado = _PedidosModel.verPedido(id);
            if (resultado != null)
                return View(resultado);
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult VerCatalogos(CatalogosEntities catalogo)
        {
            return View(_CatalogosModel.VerCatalogos(catalogo));
        }



        [HttpGet]
        public ActionResult VerCatalogo(int id)
        {
            var resultado = _CatalogosModel.VerCatalogo(id);
            if (resultado != null)
                return View(resultado);
            else
                return View("Error");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}