using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Autorepuestos.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IProductosModel _ProductosModel;

        public ProductoController(ILogger<ProductoController> logger, IProductosModel productos)
        {
            _logger = logger;
            _ProductosModel = productos;
        }

        [HttpGet]
        public ActionResult MostrarProductos(ProductosEntities producto)
        {
            return View(_ProductosModel.VerProductos(producto));
        }


        [HttpGet]
        public ActionResult AgregarProducto()
        {
            var resultado1 = _ProductosModel.ConsultaProductoCategoria();
            var resultado2 = _ProductosModel.ConsultaProductoProveedor();

            if (resultado1 != null && resultado2 != null)
            {
                var opcionesCategorias = new List<SelectListItem>();
                var opcionesProveedores = new List<SelectListItem>();


                foreach (var item in resultado1.RespuestaProductos)
                    opcionesCategorias.Add(new SelectListItem { Text = item.NombreCategoria, Value = item.IdCategoria.ToString() });

                foreach (var item in resultado2.RespuestaProductos)
                    opcionesProveedores.Add(new SelectListItem { Text = item.NombreProveedor, Value = item.IdProveedor.ToString() });

                ViewBag.ComboCategorias = opcionesCategorias;
                ViewBag.ComboProveedores = opcionesProveedores;

                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult AgregarProducto(ProductosEntities producto)
        {

            _ProductosModel.CrearProducto(producto);
            return RedirectToAction("MostrarProductos");
        }

        [HttpGet]
        public ActionResult EliminarProducto(int id)
        {
            _ProductosModel.EliminarProducto(id);
            return RedirectToAction("MostrarProductos");

        }


        [HttpGet]
        public ActionResult ModificarProducto(int id)
        {
            var resultado1 = _ProductosModel.ConsultaProductoCategoria();
            var resultado2 = _ProductosModel.ConsultaProductoProveedor();

            if (resultado1 != null && resultado2 != null)
            {
                var opcionesCategorias = new List<SelectListItem>();
                var opcionesProveedores = new List<SelectListItem>();



                foreach (var item in resultado1.RespuestaProductos)
                    opcionesCategorias.Add(new SelectListItem { Text = item.NombreCategoria, Value = item.IdCategoria.ToString() });

                foreach (var item in resultado2.RespuestaProductos)
                    opcionesProveedores.Add(new SelectListItem { Text = item.NombreProveedor, Value = item.IdProveedor.ToString() });



                ViewBag.ComboCategorias = opcionesCategorias;
                ViewBag.ComboProveedores = opcionesProveedores;

                var consulta = _ProductosModel.VerProducto(id);
                if (consulta != null)
                    return View(consulta);
                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult ModificarProducto(ProductosEntities producto)
        {
            //este llamara a la interfaz
            _ProductosModel.EditarProducto(producto);
            return RedirectToAction("MostrarProductos");

        }

    }
}
