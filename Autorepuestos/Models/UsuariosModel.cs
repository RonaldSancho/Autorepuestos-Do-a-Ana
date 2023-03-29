using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

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
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuariosEntities>("ValidarUsuario",
                    new { usuario.pCorreo, usuario.pContrasena },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public int RegitrarUsuario(UsuariosEntities usuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarUsuario",
                        new
                        {
                            usuario.pNombre,
                            usuario.pApellido1,
                            usuario.pCedula,
                            usuario.pTelefono,
                            usuario.pCorreo,
                            usuario.pContrasena,
                            usuario.pDireccion
                        },
                              commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public string CorreoExistente(string pcorreo)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query<UsuariosEntities>("CorreoExistente",
                                    new { pcorreo },
                                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (resultado == null)
                    return string.Empty;
                else
                    return null;
            }
        }

        public UsuariosEntities? RecuperarContrasenna(UsuariosEntities usuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query<UsuariosEntities>("RecuperarContrasenna", new { usuario.pCorreo }, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if (resultado.pCorreo == null)
                    return null;
                else
                {
                    return resultado;
                }
            }
        }
    }
}
