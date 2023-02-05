using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Dapper;
using MySql.Data.MySqlClient;

namespace Autorepuestos.Models
{
    public class UsuariosModel : IUsuariosModel
    {
        private readonly IConfiguration _configuration;

        public UsuariosModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UsuariosEntities? ValidarUsuarios(UsuariosEntities usuario)
        {
            using(var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuariosEntities>("ValidarUsuario",
                    new { usuario.pCorreo, usuario.pContrasena },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }
    }
}
