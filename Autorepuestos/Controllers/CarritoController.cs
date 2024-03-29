﻿using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    //Líneas para controlar el borrado de sesión y que el usuario no se devuelva
    [ResponseCache(NoStore = true, Duration = 0)]
    [FiltroSesiones]
    public class CarritoController : Controller
    {

        private readonly ILogger<CarritoController> _logger;
        private readonly ICarritoModel _CarritoModel;
        private readonly IFacturaModel _facturaModel;
        private readonly IErroresModel _ErroresModel;

        public CarritoController(ILogger<CarritoController> logger, ICarritoModel carritoModel,
            IFacturaModel facturaModel, IErroresModel errores)
        {
            _logger = logger;
            _CarritoModel = carritoModel;
            _facturaModel = facturaModel;
            _ErroresModel = errores;
        }

        [HttpGet]
        public ActionResult CarritoCompras(CarritoEntities carro)
        {
            try
            {
                var usuario = HttpContext.Session.GetInt32("IdUsuario");
                var datos = _CarritoModel.ConsultarCarrito(carro, usuario);
                HttpContext.Session.SetInt32("cant", datos.Sum(x => x.Cantidad));

                ViewBag.Total = datos.Sum(x => x.Subtotal);



                return View(datos);
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult EliminarCarrito(int id)
        {
            try
            {
                _CarritoModel.EliminarCarrito(id);
                return RedirectToAction("CarritoCompras", "Carrito");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult ProductoCarrito(int id)
        {
            try
            {
                var resultado = _CarritoModel.ProductoCarrito(id);
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

        [HttpPost]
        public ActionResult AgregarCarrito(CatalogosEntities entidad)
        {
            try
            {
                var usuario = HttpContext.Session.GetInt32("IdUsuario");
                var resultado = _CarritoModel.ConsultaExisteProductoCarrito(entidad.IdProducto, usuario);
                if (resultado != null)
                    return RedirectToAction("VerCatalogos", "Catalogo");
                else
                {
                    _CarritoModel.AgregarCarrito(entidad, usuario);
                    return RedirectToAction("VerCatalogos", "Catalogo");
                }
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult ModificarCarrito(int id)
        {
            try
            {
                var resultado = _CarritoModel.MostrarProductoCarrito(id);
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

        [HttpPost]
        public IActionResult ModificarCarrito(CarritoEntities entidad)
        {
            try
            {
                _CarritoModel.EditarCarrito(entidad);
                return RedirectToAction("CarritoCompras", "Carrito");
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        /*aqui empieza la parte del detalle como tal para verificar la compra*/
        [HttpGet]
        public IActionResult ConsultarDetalle()
        {
            try
            {
                var usuario = HttpContext.Session.GetInt32("IdUsuario");
                var consulta = _CarritoModel.ConsultarDetalle(usuario);
                if(consulta.Count > 0)
                {
                    _CarritoModel.EliminarDetalle(usuario);
                    _CarritoModel.CreandoDetalle(usuario);

                    var datos = _CarritoModel.ConsultarDetalle(usuario);
                    ViewBag.Total = datos.Sum(x => x.Subtotal);

                    return View(datos);
                }
                else
                {
                    _CarritoModel.CreandoDetalle(usuario);

                    var datos = _CarritoModel.ConsultarDetalle(usuario);
                    ViewBag.Total = datos.Sum(x => x.Subtotal);

                    return View(datos);
                }
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
            var idusuario = HttpContext.Session.GetInt32("IdUsuario");

            errores.Origen = contexto.ActionDescriptor.ControllerName + "-" + contexto.ActionDescriptor.ActionName;
            errores.Mensaje = ex.Message;
            errores.IdUsuario = HttpContext.Session.GetInt32("IdUsuario");
            _ErroresModel.RegistrarErrores(errores);
        }
    }
}
