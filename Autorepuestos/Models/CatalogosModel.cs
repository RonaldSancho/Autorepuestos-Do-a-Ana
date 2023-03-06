using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;
using static Autorepuestos.Entities.CatalogosEntities;

namespace Autorepuestos.Models
{
    public class CatalogosModel : ICatalogosModel
    {
        private readonly IConfiguration _configuration;


        public CatalogosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public CatalogosEntities? VerProductoCatalogo(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CatalogosEntities>("VisualizarCatalogo", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
        }

        public List<CatalogosEntities> VerCatalogos(CatalogosEntities catalogo)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CatalogosEntities>("VerCatalogos", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}

