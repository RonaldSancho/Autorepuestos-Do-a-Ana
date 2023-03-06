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

        public CarritoController(ILogger<CarritoController> logger, ICarritoModel carritoModel)
        {
            _logger = logger;
            _CarritoModel = carritoModel;
        }

        [HttpGet]
        public ActionResult CarritoCompras(CarritoEntities carro)
        {
            return View(_CarritoModel.ConsultarCarrito(carro));
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

        //[HttpPost]
        //public ActionResult AgregarCarrito(CatalogosEntities entidad)
        //{
        //    _CarritoModel.AgregarCarrito(entidad);
        //    return RedirectToAction("VerCatalogos", "Catalogo");
        //}

        [HttpPost]
        public ActionResult AgregarCarrito(CatalogosEntities entidad)
        {
            var resultado = _CarritoModel.ConsultaExisteProductoCarrito(entidad.IdProducto);
            if (resultado != null)
                return RedirectToAction("VerCatalogos", "Catalogo");
            else
            {
                _CarritoModel.AgregarCarrito(entidad);
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

        [HttpGet]
        public ActionResult FinalizarCompra()
        {
            _CarritoModel.FinalizarCompra();
            return RedirectToAction("VerCatalogos", "Catalogo");
        }
    }
}
