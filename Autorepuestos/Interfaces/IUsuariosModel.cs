using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface IUsuariosModel
    {
        public UsuariosEntities? ValidarUsuarios(UsuariosEntities usuario);

        public int RegitrarUsuario(UsuariosEntities usuario);

        //public string RegitrarUsuario(string pNombre, string pApellido1, string pCedula, string pTelefono, string pCorreo, string pContrasena);

        public string CorreoExistente(string pcorreo);

        public UsuariosEntities? RecuperarContrasenna(UsuariosEntities usuario);
        public void EnviarCorreo(string CorreoElectronico, string Contrasenna);

        public string CorreoInactivo(string pCorreo);
    }
}
