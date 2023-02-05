using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Autorepuestos.Models
{
    public class PedidosModel : IPedidosModel
    {
        private readonly IConfiguration _configuration;

        public PedidosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public PedidosEntities Crear(PedidosEntities pedido)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return (PedidosEntities)conexion.Query<PedidosEntities>("CrearPedido", new
                {
                    pedido.IdPedido,
                    pedido.IdProducto,
                    pedido.IdFactura,
                    pedido.Cantidad,
                    pedido.IdProveedor,
                    pedido.PrecioProducto,
                    pedido.PrecioTotal,
                    pedido.IdUsuario
                },
                    commandType: CommandType.StoredProcedure);
            }

        }

        //public List<PedidosEntities> Crear(PedidosEntities pedido)
        //{
        //    using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //        return conexion.Query<PedidosEntities>("CrearPedido", new
        //        {
        //            pedido.IdPedido,
        //            pedido.IdProducto,
        //            pedido.IdFactura,
        //            pedido.Cantidad,
        //            pedido.IdProveedor,
        //            pedido.PrecioProducto,
        //            pedido.PrecioTotal,
        //            pedido.IdUsuario
        //        },
        //            commandType: CommandType.StoredProcedure).ToList();
        //    }
        //}




        public void Editar(PedidosEntities pedido)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Query<PedidosEntities>("EditarPedido", new
                {
                    pedido.IdPedido,
                    pedido.IdProducto,
                    pedido.IdFactura,
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

        public PedidosEntities verPedido(int id)
        {
            throw new NotImplementedException();
        }

        public List<PedidosEntities> VerPedidos(PedidosEntities pedido)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<PedidosEntities>("VerPedidos", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }


    }
}

