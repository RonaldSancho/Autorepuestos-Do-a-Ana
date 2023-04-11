using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autorepuestos.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidosModel _PedidosModel;
        private readonly IErroresModel _ErroresModel;


        public PedidoController(ILogger<PedidoController> logger,IPedidosModel pedidosModel, IErroresModel erroresModel)
        {
            _logger = logger;
            _PedidosModel = pedidosModel;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public ActionResult MostrarPedido(PedidosEntities pedido)
        {
            try
            {
                return View(_PedidosModel.VerPedidos(pedido));
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AgregarPedido()
        {
            try
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
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AgregarPedido(PedidosEntities pedido)
        {
            try
            {
                _PedidosModel.Crear(pedido);
                return RedirectToAction("MostrarPedido");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EliminarPedido(int id)
        {
            try
            {
                _PedidosModel.eliminar(id);
                return RedirectToAction("MostrarPedido");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }


        [HttpGet]
        public ActionResult ModificarPedido(int id)
        {
            try
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
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ModificarPedido(PedidosEntities pedido)
        {
            try
            {
                //este llamara a la interfaz
                _PedidosModel.Editar(pedido);
                return RedirectToAction("MostrarPedido");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult DetallePedido(int id)
        {
            try
            {
                var resultado = _PedidosModel.verPedido(id);
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
            errores.IdUsuario = int.Parse(HttpContext.Session.GetString("IdUsuario"));
            _ErroresModel.RegistrarErrores(errores);
        }
    }
}
