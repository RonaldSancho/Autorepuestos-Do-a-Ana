using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Autorepuestos.Models
{
    public class CategoriaModel : ICategoriaModel
    {
        private readonly IConfiguration _configuration; 
        public CategoriaModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Crear(CategoriaEntities categoria)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearCategoria", new
                {
                    categoria.IdCategoria,
                    categoria.NombreCategoria
                },
                    commandType: CommandType.StoredProcedure);
            }

        }

        public void Editar(CategoriaEntities categoria)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarCategoria", new
                {
                    categoria.IdCategoria,
                    categoria.NombreCategoria
                },
                 commandType: CommandType.StoredProcedure);
            }
        }
        public CategoriaEntities? VerCategoria(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CategoriaEntities>("VisualizarCategoria", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
        }

        public List<CategoriaEntities> VerCategorias(CategoriaEntities categoria)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CategoriaEntities>("VerCategorias", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public RespuestaCategoria ConsultaProductoCategoria()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaProductoCategoria", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaCategoria respuesta = new RespuestaCategoria();
                List<CategoriaEntities> datos = new List<CategoriaEntities>(); if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new CategoriaEntities
                        {
                            IdCategoria = item.IdCategoria,
                            NombreCategoria = item.NombreCategoria,
                        });
                    }
                    respuesta.RespuestaCategorias = datos;
                }
                return respuesta;
            }
        }

    }
}
