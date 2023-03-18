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
        public IActionResult CrearFactura()
        {
            var usuario = HttpContext.Session.GetInt32("IdUsuario");
            _facturaModel.CrearFactura(usuario);
            return RedirectToAction("VerCatalogos", "Catalogo");
        }
    }
}
