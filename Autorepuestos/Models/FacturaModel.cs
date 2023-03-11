using Autorepuestos.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;

namespace Autorepuestos.Models
{
    public class FacturaModel : IFacturaModel
    {
        private readonly IConfiguration _configuration;


        public FacturaModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void CrearFactura(int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearFactura", new { IdUsuario }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
