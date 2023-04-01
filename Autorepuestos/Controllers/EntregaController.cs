using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autorepuestos.Controllers
{
    public class EntregaController : Controller
    {
        private readonly ILogger<EntregaController> _logger;
        private readonly IEntregasModel _EntregasModel;

        public EntregaController(ILogger<EntregaController> logger, IEntregasModel entregasModel)
        {
            _logger = logger;
            _EntregasModel = entregasModel;
        }

        [HttpGet]
        public ActionResult VerEntregas(EntregasEntities entrega)
        {
            return View(_EntregasModel.VerEntregas(entrega));
        }

        [HttpGet]
        public ActionResult AgregarEntrega()
        {
            //var result1 = _EntregasModel.ConsultaEntregaProducto();
            var result2 = _EntregasModel.ConsultaEntregaUsuario();
            if (/*result1 != null &&*/ result2 != null)
            {
                var dropdownProductos = new List<SelectListItem>();
                var dropdownUsuarios = new List<SelectListItem>();

                //foreach (var item in result1.RespuestaEntregas)
                //    dropdownProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                foreach (var item in result2.RespuestaEntregas)
                    dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                ViewBag.ComboProducto = dropdownProductos;
                ViewBag.ComboUsuario = dropdownUsuarios;
                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult AgregarEntrega(EntregasEntities entrega)
        {
            _EntregasModel.AgregarEntrega(entrega);
            return RedirectToAction("VerEntregas", "Entrega");
        }

        [HttpGet]
        public ActionResult EditarEntrega(int id)
        {
            //var result1 = _EntregasModel.ConsultaEntregaProducto();
            var result2 = _EntregasModel.ConsultaEntregaUsuario();
            if (/*result1 != null && */result2 != null)
            {
                //var dropdownProductos = new List<SelectListItem>();
                var dropdownUsuarios = new List<SelectListItem>();

                //foreach (var item in result1.RespuestaEntregas)
                //    dropdownProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });

                foreach (var item in result2.RespuestaEntregas)
                    dropdownUsuarios.Add(new SelectListItem { Text = item.NombreUsuario, Value = item.IdUsuario.ToString() });

                //ViewBag.ComboProducto = dropdownProductos;
                ViewBag.ComboUsuario = dropdownUsuarios;

                var consulta = _EntregasModel.ConsultarEntrega(id);
                if (consulta != null)
                {
                    return View(consulta);
                }
                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult EditarEntrega(EntregasEntities entrega)
        {
            _EntregasModel.Editar(entrega);
            return RedirectToAction("VerEntregas", "Entrega");
        }

        [HttpGet]
        public ActionResult EliminarEntrega(int id)
        {
            _EntregasModel.EliminarEntrega(id);
            return RedirectToAction("VerEntregas", "Entrega");

        }
    }
}
