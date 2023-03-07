using Autorepuestos.Entities;
using Autorepuestos.Interfaces;
using Autorepuestos.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using MimeKit.Text;
using MimeKit;
using MySql.Data.MySqlClient;
using MailKit.Net.Smtp;
using System.Data.SqlClient;

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

        public int RegitrarUsuario(UsuariosEntities usuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Execute("RegistrarUsuario",
                        new { usuario.pNombre, usuario.pApellido1, usuario.pCedula, usuario.pTelefono,
                              usuario.pCorreo, usuario.pContrasena },
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
                return conexion.Query<UsuariosEntities>("CorreoExistente",
                    new { usuario.pCorreo },
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public void RecuperarContrasennaCorreo(string Correo, string Contrasena)
        {
            string Email = _configuration.GetSection("EmailConfiguracion:Email").Value;
            string titulo = _configuration.GetSection("EmailConfiguracion:Titulo").Value;
            string Contraseña = _configuration.GetSection("EmailConfiguracion:Contraseña").Value;
            string Host = _configuration.GetSection("EmailConfiguracion:Host").Value;
            string Puerto = _configuration.GetSection("EmailConfiguracion:Puerto").Value;

            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(Email));
            email.To.Add(MailboxAddress.Parse(Correo));
            email.Subject = titulo;
            email.Body = new TextPart(TextFormat.Html)
            { Text = "<h1>La contraseña del usuario " + Correo + " es " + Contrasena + "</h1>" };

            using var smtp = new SmtpClient();

            //smtp.CheckCertificateRevocation = false;
            smtp.Connect(Email, 587, false);
            smtp.Authenticate(Email, Contraseña);
            smtp.Send(email);
            smtp.Disconnect(true);

        }
    }
}
