using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Autorepuestos.Controllers
{
    //Líneas para controlar el borrado de sesión y que el usuario no se devuelva
    [ResponseCache(NoStore = true, Duration = 0)]
    [FiltroSesiones]
    public class ProveedorController : Controller
    {
        private readonly ILogger<ProveedorController> _logger;
        private readonly IProveedorModel _ProveedorModel;
        private readonly IErroresModel _ErroresModel;


        public ProveedorController(ILogger<ProveedorController> logger, IProveedorModel proveedorModel, IErroresModel erroresModel)
        {
            _logger = logger;
            _ProveedorModel = proveedorModel;
            _ErroresModel = erroresModel;
        }

        [HttpGet]
        public ActionResult MostrarProveedor(ProveedorEntities proveedor)
        {
            try
            {
                return View(_ProveedorModel.VerProveedores(proveedor));
            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AgregarProveedor()
        {
            try
            {

                return View();

            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult AgregarProveedor(ProveedorEntities proveedor)
        {
            ModelState.Remove("IdProveedor");

            if (ModelState.IsValid)
            {
                try
                {
                    _ProveedorModel.Crear(proveedor);
                    TempData["MensajeProveedorAgregado"] = true;
                    return RedirectToAction("AgregarProveedor");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }

            else //Con estas nuevas líneas se recarga el DropDown
            {
                
                    return View();
            }
        }



        [HttpGet]
        public ActionResult ModificarProveedor(int id)
        {
            try
            {

                var consulta = _ProveedorModel.VerProveedor(id);
                if (consulta != null)
                    return View(consulta);
                return View();

            }
            catch (Exception ex)
            {
                RegistrarErrores(ex, ControllerContext);
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult ModificarProveedor(ProveedorEntities proveedor)
        {
            ModelState.Remove("pIdProveedor");
            if (ModelState.IsValid)
            {
                try
                {
                    //este llamara a la interfaz
                    _ProveedorModel.Editar(proveedor);
                    TempData["MensajeModificacionProveedor"] = true;
                    return RedirectToAction("ModificarProveedor");
                }
                catch (Exception ex)
                {
                    RegistrarErrores(ex, ControllerContext);
                    return View("Error");
                }
            }
            else
            {

                return View();
            }
        }

        [HttpGet]
        public ActionResult DetalleProveedor(int id)
        {
            try
            {
                var resultado = _ProveedorModel.VerProveedor(id);
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