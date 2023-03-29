using System.ComponentModel.DataAnnotations;

namespace Autorepuestos.Entities
{
    public class UsuariosEntities : ValidationAttribute
    {
        public int IdUsuario { get; set; }
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre no puede contener menos de 2 letras")]
        public string pNombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es requerido.")]
        [MinLength(2, ErrorMessage = "El apellido no puede contener menos de 2 letras")]
        public string pApellido1 { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cédula es requerido.")]
        [StringLength(9, ErrorMessage = "La cédula debe tener 9 dígitos", MinimumLength = 9)]
        public string pCedula { get; set; } = string.Empty;

        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(8, ErrorMessage = "El número de teléfono debe contener 8 dígitos", MinimumLength = 8)]
        public string pTelefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es requerido.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Digíte un correo válido")]
        public string pCorreo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es requerida")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "La contraseña debe contener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un número y un carácter especial")]
        public string pContrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "La dirección es requerida.")]
        [MinLength(15, ErrorMessage = "La dirección debe ser específica por lo que debe tener más de 15 caracteres")]
        public string pDireccion { get; set; } = string.Empty;

        //Falta IdRol y poner correctamente el valor de IdUsuario

    }
}
