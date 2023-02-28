using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace Autorepuestos.Models
{
    public class CarritoModel : ICarritoModel
    {

        private readonly IConfiguration _configuration;


        public CarritoModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<CarritoEntities> ConsultarCarrito(CarritoEntities carro)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("ConsultarCarrito", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public CarritoEntities? EliminarCarrito(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("EliminarCarrito", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void AgregarCarrito(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Query<CarritoEntities>("AgregarCarrito", new { id }, commandType: CommandType.StoredProcedure);
            }
        }

        //public void AgregarCarrito(int id, int cant)
        //{
        //    using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //         conexion.Query<CarritoEntities>("AgregarCarrito", new { id , cant}, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //    }
        //}

        //public CarritoEntities? MostrarProductoCarrito(int id)
        //{
        //    using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
        //    {
        //       return conexion.Query<CarritoEntities>("MostrarProductoCarrito", new { id}, commandType: CommandType.StoredProcedure).FirstOrDefault();
        //    }
        //}

        public void FinalizarCompra()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("FinalizarCompra", new {  }, commandType: CommandType.StoredProcedure);
            }
        }

    }
}
