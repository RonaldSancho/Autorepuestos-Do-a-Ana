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
            return RedirectToAction("CarritoCompras","Carrito");
        }

        [HttpGet]
        public ActionResult AgregarCarrito(int id)
        {
            _CarritoModel.AgregarCarrito(id);
            return RedirectToAction("VerCatalogos","Catalogo");
        }
    }
}
