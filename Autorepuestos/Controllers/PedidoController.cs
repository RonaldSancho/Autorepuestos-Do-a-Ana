﻿using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autorepuestos.Controllers
{
    //Líneas para controlar el borrado de sesión y que el usuario no se devuelva
    [ResponseCache(NoStore = true, Duration = 0)]
    [FiltroSesiones]
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;
        private readonly IPedidosModel _PedidosModel;
        private readonly IErroresModel _ErroresModel;
        private readonly IProveedorModel _ProveedorModel;



        public PedidoController(ILogger<PedidoController> logger,IPedidosModel pedidosModel, IErroresModel erroresModel, IProveedorModel proveedorModel)
        {
            _logger = logger;
            _PedidosModel = pedidosModel;
            _ErroresModel = erroresModel;
            _ProveedorModel = proveedorModel;

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
                var resultado2 = _ProveedorModel.ConsultaPedidoProveedor();
                var resultado3 = _PedidosModel.ConsultaPedidoUsuario();
                if (resultado1 != null && resultado2 != null && resultado3 != null)
                {
                    var opcionesProductos = new List<SelectListItem>();
                    var opcionesProveedores = new List<SelectListItem>();
                    var opcionesUsuarios = new List<SelectListItem>();

                    foreach (var item in resultado1.RespuestaPedidos)
                        opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                    foreach (var item in resultado2.RespuestaProveedores)
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
            if (ModelState.IsValid)
            {
                try
                {
                    _PedidosModel.Crear(pedido);
                    TempData["MensajePedidoAgregado"] = true;
                    return RedirectToAction("AgregarPedido");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else
            {
                var resultado1 = _PedidosModel.ConsultaPedidoProducto();
                var resultado2 = _ProveedorModel.ConsultaPedidoProveedor();
                var resultado3 = _PedidosModel.ConsultaPedidoUsuario();
                if (resultado1 != null && resultado2 != null && resultado3 != null)
                {
                    var opcionesProductos = new List<SelectListItem>();
                    var opcionesProveedores = new List<SelectListItem>();
                    var opcionesUsuarios = new List<SelectListItem>();

                    foreach (var item in resultado1.RespuestaPedidos)
                        opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                    foreach (var item in resultado2.RespuestaProveedores)
                        opcionesProveedores.Add(new SelectListItem { Text = item.NombreProveedor, Value = item.IdProveedor.ToString() });

                    foreach (var item in resultado3.RespuestaPedidos)
                        opcionesUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboProductos = opcionesProductos;
                    ViewBag.ComboProveedores = opcionesProveedores;
                    ViewBag.ComboUsuarios = opcionesUsuarios;
                    return View(pedido);
                }
                else
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
                var resultado2 = _ProveedorModel.ConsultaPedidoProveedor();
                var resultado3 = _PedidosModel.ConsultaPedidoUsuario();
                if (resultado1 != null && resultado2 != null && resultado3 != null)
                {
                    var opcionesProductos = new List<SelectListItem>();
                    var opcionesProveedores = new List<SelectListItem>();
                    var opcionesUsuarios = new List<SelectListItem>();

                    foreach (var item in resultado1.RespuestaPedidos)
                        opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                    foreach (var item in resultado2.RespuestaProveedores)
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
            ModelState.Remove("pIdPedido");
            if (ModelState.IsValid)
            {
                try
                {
                    //este llamara a la interfaz
                    _PedidosModel.Editar(pedido);
                    TempData["MensajeModificacionPedido"] = true;
                    return RedirectToAction("ModificarPedido");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else
            {
                var resultado1 = _PedidosModel.ConsultaPedidoProducto();
                var resultado2 = _ProveedorModel.ConsultaPedidoProveedor();
                var resultado3 = _PedidosModel.ConsultaPedidoUsuario();
                if (resultado1 != null && resultado2 != null && resultado3 != null)
                {
                    var opcionesProductos = new List<SelectListItem>();
                    var opcionesProveedores = new List<SelectListItem>();
                    var opcionesUsuarios = new List<SelectListItem>();

                    foreach (var item in resultado1.RespuestaPedidos)
                        opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                    foreach (var item in resultado2.RespuestaProveedores)
                        opcionesProveedores.Add(new SelectListItem { Text = item.NombreProveedor, Value = item.IdProveedor.ToString() });

                    foreach (var item in resultado3.RespuestaPedidos)
                        opcionesUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                    ViewBag.ComboProductos = opcionesProductos;
                    ViewBag.ComboProveedores = opcionesProveedores;
                    ViewBag.ComboUsuarios = opcionesUsuarios;

                    return View(pedido);
                }
                else
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
            errores.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            _ErroresModel.RegistrarErrores(errores);
        }
    }
}
