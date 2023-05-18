using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using static Autorepuestos.Entities.UsuariosEntities;
using System.Data;


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
                            usuario.pContrasena
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

        public void ModificarUsuario(UsuariosEntities usuario)

        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                if (usuario.pContrasena == null)
                    usuario.pContrasena = "";
                conexion.Execute("ModificarUsuario", new
                {
                    usuario.IdUsuario,
                    usuario.pIdRol,
                    usuario.pNombre,
                    usuario.pApellido1,
                    usuario.pCedula,
                    usuario.pTelefono,
                    usuario.pCorreo,
                    usuario.pContrasena
                },
                commandType: CommandType.StoredProcedure);
            }
        }

        public UsuariosEntities? VerUsuario(int id)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuariosEntities>("VerUsuario", new { id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public List<UsuariosEntities> VerUsuarios(UsuariosEntities usuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<UsuariosEntities>("VerUsuarios", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public RespuestaUsuario ConsultarRol()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultarRol", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaUsuario respuesta = new RespuestaUsuario();
                List<UsuariosEntities> datos = new List<UsuariosEntities>(); if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new UsuariosEntities
                        {
                            pIdRol = item.IdRol,
                            NombreRol = item.NombreRol,
                        });
                    }
                    respuesta.RespuestaUsuarios = datos;
                }
                return respuesta;
            
            }
        }

        public void CambiarEstadoUsuario(int IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CambiarEstadoUsuario", new { IdUsuario }, commandType: CommandType.StoredProcedure);
            }

        }

    }
}
