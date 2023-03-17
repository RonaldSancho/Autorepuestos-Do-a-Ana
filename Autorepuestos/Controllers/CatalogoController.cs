﻿using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace Autorepuestos.Controllers
{
    [ResponseCache (NoStore =true, Duration = 0)]
    public class CatalogoController : Controller
    {

        private readonly ILogger<CatalogoController> _logger;
        private readonly ICatalogosModel _CatalogosModel;
        private readonly IUsuariosModel _UsuariosModel;

        public CatalogoController(ILogger<CatalogoController> logger, ICatalogosModel catalogoModel, IUsuariosModel usuariosModel)
        {
            _logger = logger;
            _CatalogosModel = catalogoModel;
            _UsuariosModel = usuariosModel;
        }

        [HttpPost]
        public IActionResult VerCatalogos(CatalogosEntities catalogo, UsuariosEntities usuarios)
        {
            try
            {
                var resultado = _UsuariosModel.ValidarUsuarios(usuarios);
                if (resultado != null)
                {
                    HttpContext.Session.SetInt32("IdUsuario", resultado.IdUsuario);
                    return RedirectToAction("VerCatalogos", "Catalogo");
                }
                else
                {
                    ViewBag.mensaje = "Valide sus credenciales por favor.";
                    return RedirectToAction("Index","Home");
                }
            }
            catch(Exception ex)
            {
                ViewBag.mensaje = "Se presentó un inconveniente con el sistema.";
                return RedirectToAction("Index","Home");
            }

            
        }

        [HttpGet]
        [FiltroSesiones]
        public IActionResult VerCatalogos(CatalogosEntities catalogo)
        {
            return View(_CatalogosModel.VerCatalogos(catalogo));
        }

        //ESCRIBIR ANTES DE ESTA LÍNEA//

        //[HttpGet]
        //public ActionResult VerCatalogos()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult Index(UsuariosEntities usuario)
        //{

        //    if (_UsuariosModel.ValidarUsuarios(usuario) != null)
        //        return View();
        //    else
        //        return View("Index");
        //}

        //[HttpPost]
        //public ActionResult VerCatalogos(CatalogosEntities catalogo)
        //{
        //    return View(_CatalogosModel.VerCatalogos(catalogo));
        //}

        //[HttpGet]
        //public ActionResult VerCatalogos()
        //{
        //    return View(_CatalogosModel.VerCatalogos(new CatalogosEntities()));
        //}
    }
}
