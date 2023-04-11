using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data;
using Autorepuestos.Interfaces;

namespace Autorepuestos.Models
{
    public class ErroresModel : IErroresModel
    {
        private readonly IConfiguration _configuration;

        public ErroresModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RegistrarErrores(ErroresEntities errores)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("RegistrarErrores",
                    new { errores.Mensaje, errores.Origen, errores.IdUsuario },
                    commandType: System.Data.CommandType.StoredProcedure);
            }
        }
    }
}
