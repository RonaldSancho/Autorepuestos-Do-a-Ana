using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Autorepuestos.Controllers
{
    public class FacturaController : Controller
    {

        private readonly IFacturaModel _facturaModel;
        private readonly ILogger<FacturaController> _logger;
        public FacturaController(IFacturaModel facturaModel, ILogger<FacturaController> logger)
        {
            _facturaModel = facturaModel;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult VerFacturas()
        {
            return View(_facturaModel.VerFacturas());
        }

        [HttpGet]
        public IActionResult CrearFactura()
        {
            var usuario = HttpContext.Session.GetInt32("IdUsuario");
            _facturaModel.CrearFactura(usuario);
            return RedirectToAction("VerCatalogos", "Catalogo");
        }

        [HttpGet]
        public IActionResult EliminarFactura(int id) 
        {
            _facturaModel.EliminarFactura(id);
            return RedirectToAction("VerFacturas", "Factura");
        }

        [HttpGet]
        public IActionResult VerDetalleFactura(int id)
        {
            var resultado = _facturaModel.VerDetalleFactura(id);
            if (resultado != null)
                return View(resultado);
            else
                return View("Error");
        }

    }
}
