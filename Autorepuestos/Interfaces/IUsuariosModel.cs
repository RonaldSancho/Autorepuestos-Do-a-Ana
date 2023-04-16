using Autorepuestos.Entities;
using static Autorepuestos.Entities.UsuariosEntities;

namespace Autorepuestos.Interfaces
{
    public interface IUsuariosModel
    {
        public UsuariosEntities? ValidarUsuarios(UsuariosEntities usuario);

        public int RegitrarUsuario(UsuariosEntities usuario);

        public string CorreoExistente(string pcorreo);

        public UsuariosEntities? RecuperarContrasenna(UsuariosEntities usuario);

        public void ModificarUsuario(UsuariosEntities usuario);
        public UsuariosEntities? VerUsuario(int id);
        public List<UsuariosEntities> VerUsuarios(UsuariosEntities usuario);
        public RespuestaUsuario ConsultarRol();
        public void CambiarEstadoUsuario(int IdUsuario);

        //public UsuariosEntities? RecuperarContrasenna(UsuariosEntities usuario);
        //public void EnviarCorreo(string CorreoElectronico, string Contrasenna);

        //public string CorreoInactivo(string pCorreo);
    }
}
