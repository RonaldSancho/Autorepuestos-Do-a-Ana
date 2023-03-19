using Autorepuestos.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Autorepuestos.Entities;

namespace Autorepuestos.Models
{
    public class FacturaModel : IFacturaModel
    {
        private readonly IConfiguration _configuration;


        public FacturaModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public List<FacturaEntities> VerFacturas()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
               return conexion.Query<FacturaEntities>("VerFacturas", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }
        public void CrearFactura(int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearFactura", new { IdUsuario }, commandType: CommandType.StoredProcedure);
            }
        }

        public void EliminarFactura(int IdFactura)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EliminarFactura", new { IdFactura }, commandType: CommandType.StoredProcedure);
            }

        }

        public FacturaEntities? VerDetalleFactura(int IdFactura)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<FacturaEntities>("VerDetalleFactura", new { IdFactura }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
