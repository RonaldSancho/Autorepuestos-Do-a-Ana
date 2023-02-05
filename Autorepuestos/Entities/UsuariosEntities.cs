using System.ComponentModel.DataAnnotations;

namespace Autorepuestos.Entities
{
    public class UsuariosEntities : ValidationAttribute
    {
        //public int IdUsuario { get; set; };
        public string Nombre { get; set; } = string.Empty;

        public string Apellido1 { get; set; } = string.Empty;

        public string Apellido2 { get; set; } = string.Empty;

        public string Cedula { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es requerido.")]
        [EmailAddress(ErrorMessage = "Digíte un correo válido")]
        public string pCorreo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        public string pContrasena { get; set; } = string.Empty;

        //Falta IdRol y poner correctamente el valor de IdUsuario

    }
}
