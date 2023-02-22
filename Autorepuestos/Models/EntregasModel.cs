using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using static Autorepuestos.Entities.EntregasEntities;

namespace Autorepuestos.Models
{
    public class EntregasModel : IEntregasModel
    {
        private readonly IConfiguration _configuration;

        public EntregasModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void AgregarEntrega(EntregasEntities entrega)
        {
            using (var conexion= new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("AgregarEntrega", new
                {
                    entrega.IdProducto,
                    entrega.DireccionEntrega,
                    entrega.IdUsuario,
                    entrega.Estado
                },
                    commandType: CommandType.StoredProcedure);
            }
        }

        public RespuestaEntrega ConsultaEntregaProducto()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaPedidoProducto", new { }, commandType: CommandType.StoredProcedure).ToList();//reutilizo un procedimiento almacenado, es por eso el nombre..
                RespuestaEntrega respuesta= new RespuestaEntrega();
                List<EntregasEntities> datos = new List<EntregasEntities>();
                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new EntregasEntities
                        {
                            IdProducto = item.IdProducto,
                            NombreProducto = item.NombreProducto,
                        });
                    }
                    respuesta.RespuestaEntregas = datos;
                }
                return respuesta;
            }
        }

        public RespuestaEntrega ConsultaEntregaUsuario()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaPedidoUsuario", new { }, commandType: CommandType.StoredProcedure).ToList();//reutilizo un procedimiento almacenado, es por eso el nombre..
                RespuestaEntrega respuesta = new RespuestaEntrega();
                List<EntregasEntities> datos = new List<EntregasEntities>();

                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new EntregasEntities
                        {
                            IdUsuario = item.IdUsuario,
                            NombreUsuario = item.NombreUsuario,
                        });
                    }
                    respuesta.RespuestaEntregas = datos;
                }
                return respuesta;
            }
        }

        public void Editar(EntregasEntities entrega)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarEntrega", new
                {
                    entrega.pIdEntrega,
                    entrega.IdProducto,
                    entrega.DireccionEntrega,
                    entrega.IdUsuario,
                    entrega.Estado
                },
                 commandType: CommandType.StoredProcedure);
            }
        }

        public EntregasEntities? ConsultarEntrega (int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<EntregasEntities>("ConsultarEntrega", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
        }

        public void EliminarEntrega(int id) 
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EliminarEntrega", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<EntregasEntities> VerEntregas(EntregasEntities entrega)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<EntregasEntities>("VerEntregas", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
    }
}
