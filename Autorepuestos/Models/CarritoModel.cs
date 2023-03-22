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

        public List<CarritoEntities> ConsultarCarrito(CarritoEntities carro, int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("ConsultarCarrito", new { IdUsuario}, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public CarritoEntities? EliminarCarrito(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("EliminarCarrito", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public CatalogosEntities? ProductoCarrito(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CatalogosEntities>("ProductoCarrito", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void AgregarCarrito(CatalogosEntities entidad, int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("AgregarCarrito", new { entidad.IdProducto, entidad.Cantidad, entidad.Precio, IdUsuario }, commandType: CommandType.StoredProcedure);
            }
        }

        public void EditarCarrito(CarritoEntities entidad)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EditarCarrito", new { entidad.IdCarrito, entidad.Cantidad, entidad.Precio }, commandType: CommandType.StoredProcedure);
            }
        }

        public CarritoEntities? MostrarProductoCarrito(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("MostrarProductoCarrito", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public CarritoEntities? ConsultaExisteProductoCarrito(int id, int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("ConsultaExisteProductoCarrito", new { id, IdUsuario}, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        /*aqui empieza la parte del detalle como tal para verificar la compra*/
        public void CreandoDetalle()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CreandoDetalle", new { }, commandType: CommandType.StoredProcedure);
            }
        }

        public List<CarritoEntities> ConsultarDetalle(int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<CarritoEntities>("ConsultarDetalle", new { IdUsuario }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

    }
}
