using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using static Autorepuestos.Entities.PedidosEntities;

namespace Autorepuestos.Models
{
    public class ProveedorModel : IProveedorModel
    {
        private readonly IConfiguration _configuration; public ProveedorModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Crear(ProveedorEntities proveedor)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearProveedor", new
                {
                    proveedor.IdProveedor,
                    proveedor.NombreProveedor,
                    proveedor.Telefono,
                    proveedor.Direccion
                },
                    commandType: CommandType.StoredProcedure);
            }

        }

        public void Editar(ProveedorEntities proveedor)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarProveedor", new
                {
                    proveedor.IdProveedor,
                    proveedor.NombreProveedor,
                    proveedor.Telefono,
                    proveedor.Direccion
                },
                 commandType: CommandType.StoredProcedure);
            }
        }
        public ProveedorEntities? VerProveedor(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProveedorEntities>("VisualizarProveedor", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
        }

        public List<ProveedorEntities> VerProveedores(ProveedorEntities proveedor)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<ProveedorEntities>("VerProveedores", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public RespuestaProveedor ConsultaProductoProveedor()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaProductoProveedor", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaProveedor respuesta = new RespuestaProveedor();
                List<ProveedorEntities> datos = new List<ProveedorEntities>(); if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new ProveedorEntities
                        {
                            IdProveedor = item.IdProveedor,
                            NombreProveedor = item.NombreProveedor,
                        });
                    }
                    respuesta.RespuestaProveedores = datos;
                }
                return respuesta;
            }
        }

        public RespuestaProveedor ConsultaPedidoProveedor()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaPedidoProveedor", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaProveedor respuesta = new RespuestaProveedor();
                List<ProveedorEntities> datos = new List<ProveedorEntities>();

                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new ProveedorEntities
                        {
                            IdProveedor = item.IdProveedor,
                            NombreProveedor = item.NombreProveedor,
                        });
                    }
                    respuesta.RespuestaProveedores = datos;
                }
                return respuesta;
            }
        }
    }
}
