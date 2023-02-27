using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autorepuestos.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidosModel _PedidosModel;


        public PedidoController(ILogger<PedidoController> logger,IPedidosModel pedidosModel)
        {
            _logger = logger;
            _PedidosModel = pedidosModel;
        }

        [HttpGet]
        public ActionResult MostrarPedido(PedidosEntities pedido)
        {
            return View(_PedidosModel.VerPedidos(pedido));
        }

        [HttpGet]
        public ActionResult AgregarPedido()
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
        public ActionResult AgregarPedido(PedidosEntities pedido)
        {

            _PedidosModel.Crear(pedido);
            return RedirectToAction("MostrarPedido");
        }

        [HttpGet]
        public ActionResult EliminarPedido(int id)
        {
            _PedidosModel.eliminar(id);
            return RedirectToAction("MostrarPedido");

        }


        [HttpGet]
        public ActionResult ModificarPedido(int id)
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
        public ActionResult ModificarPedido(PedidosEntities pedido)
        {
            //este llamara a la interfaz
            _PedidosModel.Editar(pedido);
            return RedirectToAction("MostrarPedido");

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

    }
}
