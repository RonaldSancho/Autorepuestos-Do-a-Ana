using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Principal;

namespace Autorepuestos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IUsuariosModel _usuariosModel;

        public UsuarioController(ILogger<UsuarioController> logger, IUsuariosModel usuariosModel)
        {
            _logger = logger;
            _usuariosModel = usuariosModel;
            
        }
        [HttpGet]
        public ActionResult VerUsuarios(UsuariosEntities usuario)
        {
            return View(_usuariosModel.VerUsuarios(usuario));
        }
        [HttpGet]
        public ActionResult ModificarUsuario(int id)
        {
            var resultado = _usuariosModel.ConsultarRol();
            

            if (resultado != null )
            {
                var opcionesRol = new List<SelectListItem>();
                foreach (var item in resultado.RespuestaUsuarios)
                    opcionesRol.Add(new SelectListItem { Text = item.NombreRol, Value = item.pIdRol.ToString() });

                
                ViewBag.ComboRol = opcionesRol;

                var consulta = _usuariosModel.VerUsuario(id);
                if (consulta != null)
                    return View(consulta);
                return View();
            }
            else
                return View("Error");
        }

        [HttpPost]
        public ActionResult ModificarUsuario(UsuariosEntities usuario)
        {
            
                _usuariosModel.ModificarUsuario(usuario);
            
            //este llamara a la interfaz
            return RedirectToAction("VerUsuarios");

        }
    }
}
