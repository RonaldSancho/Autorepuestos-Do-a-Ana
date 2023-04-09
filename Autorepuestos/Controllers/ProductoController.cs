using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;

namespace Autorepuestos.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ILogger<ProductoController> _logger;
        private readonly IProductosModel _ProductosModel;
        private readonly IPedidosModel _PedidosModel;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductoController(ILogger<ProductoController> logger, IProductosModel productos, IPedidosModel pedidosModel,IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _ProductosModel = productos;
            _PedidosModel = pedidosModel;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult MostrarProductos(ProductosEntities producto)
        {
            return View(_ProductosModel.VerProductos(producto));
        }


        [HttpGet]
        public IActionResult AgregarProducto()
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
        public IActionResult AgregarProducto(ProductosEntities producto)
        {
            
            if (producto.ImagenDatos != null)
            {
                string folder = "/template/img/";
                folder +=  producto.ImagenDatos.FileName;
                producto.Imagen = "~" + folder;

                string serverFolder = _webHostEnvironment.WebRootPath+ folder;
                producto.ImagenDatos.CopyTo(new FileStream(serverFolder,FileMode.Create));
                _ProductosModel.CrearProducto(producto);
            }
            else
            {
                _ProductosModel.CrearProducto(producto);
            }
            
            
            return RedirectToAction("MostrarProductos");
        }

        [HttpGet]
        public IActionResult EliminarProducto(int id)
        {
            _ProductosModel.EliminarProducto(id);
            return RedirectToAction("MostrarProductos");

        }


        [HttpGet]
        public IActionResult ModificarProducto(int id)
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
        public IActionResult ModificarProducto(ProductosEntities producto)
        {
            if (producto.ImagenDatos != null)
            {
                string folder = "/template/img/";
                folder += producto.ImagenDatos.FileName;
                producto.ImagenN = "~" + folder;

                string serverFolder = _webHostEnvironment.WebRootPath + folder;
                producto.ImagenDatos.CopyTo(new FileStream(serverFolder, FileMode.Create));
                _ProductosModel.EditarProducto(producto);
            }
            else
            {
                _ProductosModel.EditarProducto(producto);
            }
            //este llamara a la interfaz
            return RedirectToAction("MostrarProductos");

        }

        [HttpGet]
        public IActionResult DevolucionProducto()
        {
            try
            {
                var resultado = _PedidosModel.ConsultaPedidoProducto();
                if (resultado != null)
                {
                    var opcionesProductos = new List<SelectListItem>();

                    foreach (var item in resultado.RespuestaPedidos)
                        opcionesProductos.Add(new SelectListItem { Text = item.NombreProducto, Value = item.IdProducto.ToString() });
                    ViewBag.ComboProductos = opcionesProductos;
                }
                return View();
            }
            catch (Exception ex)
            {
                return View("Error");   
            }
        }
        [HttpPost]
        public IActionResult DevolucionProducto(ProductosEntities entidad)
        {
            try
            {
                _ProductosModel.DevolucionProducto(entidad);
                return RedirectToAction("MostrarProductos", "Producto");
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }



    }
}
