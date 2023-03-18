using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    public class CarritoController : Controller
    {

        private readonly ILogger<CarritoController> _logger;
        private readonly ICarritoModel _CarritoModel;
        private readonly IFacturaModel _facturaModel;

        public CarritoController(ILogger<CarritoController> logger, ICarritoModel carritoModel, IFacturaModel facturaModel)
        {
            _logger = logger;
            _CarritoModel = carritoModel;
            _facturaModel = facturaModel;
        }

        [HttpGet]
        public ActionResult CarritoCompras(CarritoEntities carro)

        {
            var usuario = HttpContext.Session.GetInt32("IdUsuario");
            var datos = _CarritoModel.ConsultarCarrito(carro, usuario);
            ViewBag.Total = datos.Sum(x => x.Subtotal);

            return View(datos);
        }

        [HttpGet]
        public ActionResult EliminarCarrito(int id)
        {
            _CarritoModel.EliminarCarrito(id);
            return RedirectToAction("CarritoCompras", "Carrito");
        }

        [HttpGet]
        public ActionResult ProductoCarrito(int id)
        {
            var resultado = _CarritoModel.ProductoCarrito(id);
            if (resultado != null)
                return View(resultado);
            else
                return View("Error");

        }

        [HttpPost]
        public ActionResult AgregarCarrito(CatalogosEntities entidad)
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

        [HttpGet]
        public IActionResult ModificarCarrito(int id)
        {
            var resultado = _CarritoModel.MostrarProductoCarrito(id);
            if (resultado != null)
                return View(resultado);
            else
                return View("Error");
        }

        [HttpPost]
        public IActionResult ModificarCarrito(CarritoEntities entidad)
        {
            _CarritoModel.EditarCarrito(entidad);
            return RedirectToAction("CarritoCompras", "Carrito");
        }
    }
}
