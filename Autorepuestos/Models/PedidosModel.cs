using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MySql.Data.MySqlClient;
using System.Data;
using static Autorepuestos.Entities.PedidosEntities;

namespace Autorepuestos.Models
{
    public class PedidosModel : IPedidosModel
    {
        private readonly IConfiguration _configuration;

        public PedidosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void Crear(PedidosEntities pedido)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearPedido", new
                {
                    pedido.IdProducto,
                    pedido.Cantidad,
                    pedido.IdProveedor,
                    pedido.PrecioProducto,
                    pedido.PrecioTotal,
                    pedido.IdUsuario
                },
                    commandType: CommandType.StoredProcedure);
            }

        }

        public void Editar(PedidosEntities pedido)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarPedido", new
                {
                    pedido.pIdPedido,
                    pedido.IdProducto,
                    pedido.Cantidad,
                    pedido.IdProveedor,
                    pedido.PrecioProducto,
                    pedido.PrecioTotal,
                    pedido.IdUsuario
                },
                 commandType: CommandType.StoredProcedure);
            }
        }

        public void eliminar(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EliminarPedido", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        public PedidosEntities? verPedido(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<PedidosEntities>("VisualizarPedido", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
        }

        public List<PedidosEntities> VerPedidos(PedidosEntities pedido)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<PedidosEntities>("VerPedidos", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public RespuestaPedido ConsultaPedidoProducto()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaPedidoProducto", new { }, commandType: CommandType.StoredProcedure).ToList();

                RespuestaPedido respuesta = new RespuestaPedido();
                List<PedidosEntities> datos = new List<PedidosEntities>();

                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new PedidosEntities
                        {
                            IdProducto = item.IdProducto,
                            NombreProducto = item.NombreProducto,
                        });
                    }

                    respuesta.RespuestaPedidos = datos;
                }

                return respuesta;
            }
        }

        

        public RespuestaPedido ConsultaPedidoUsuario()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultaPedidoUsuario", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaPedido respuesta = new RespuestaPedido();
                List<PedidosEntities> datos = new List<PedidosEntities>();

                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new PedidosEntities
                        {
                            IdUsuario = item.IdUsuario,
                            NombreUsuario = item.NombreUsuario,
                        });
                    }
                    respuesta.RespuestaPedidos = datos;
                }
                return respuesta;
            }
        }


    }
}

