using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;
using static Autorepuestos.Entities.ProductosEntities;
namespace Autorepuestos.Models
{
    public class ProductosModel : IProductosModel
    {
        private readonly IConfiguration _configuration; public ProductosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CrearProducto(ProductosEntities producto)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearProducto", new
                {
                    producto.NombreProducto,
                    producto.Descripcion,
                    producto.Precio,
                    producto.IdCategoria,
                    producto.Existencias,
                    producto.IdProveedor
                },
                commandType: CommandType.StoredProcedure);
            }
        }

        public void EditarProducto(ProductosEntities producto)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarProducto", new
                {
                    producto.pIdProducto,
                    producto.NombreProducto,
                    producto.Descripcion,
                    producto.Precio,
                    producto.IdCategoria,
                    producto.Existencias,
                    producto.IdProveedor
                },
                commandType: CommandType.StoredProcedure);
            }
        }

        public void EliminarProducto(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EliminarProducto", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public ProductosEntities? VerProducto(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProductosEntities>("VisualizarProducto", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<ProductosEntities> VerProductos(ProductosEntities productos)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProductosEntities>("VerProductos", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public RespuestaProducto ConsultaProductoCategoria()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaProductoCategoria", new { }, commandType: CommandType.StoredProcedure).ToList(); RespuestaProducto respuesta = new RespuestaProducto();
                List<ProductosEntities> datos = new List<ProductosEntities>(); if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new ProductosEntities
                        {
                            IdCategoria = item.IdCategoria,
                            NombreCategoria = item.NombreCategoria,
                        });
                    }
                    respuesta.RespuestaProductos = datos;
                }
                return respuesta;
            }
        }

        public RespuestaProducto ConsultaProductoProveedor()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaProductoProveedor", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaProducto respuesta = new RespuestaProducto();
                List<ProductosEntities> datos = new List<ProductosEntities>(); if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new ProductosEntities
                        {
                            IdProveedor = item.IdProveedor,
                            NombreProveedor = item.NombreProveedor,
                        });
                    }
                    respuesta.RespuestaProductos = datos;
                }
                return respuesta;
            }
        }
    }
}