﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autorepuestos.Entities
{
    public class UsuariosEntities : ValidationAttribute
    {
        public int IdUsuario { get; set; }
        [DisplayName("Nombre")]
        [Required(ErrorMessage = "El nombre es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre no puede contener menos de 2 letras")]
        public string pNombre { get; set; } = string.Empty;
        [DisplayName("Apellido")]
        [Required(ErrorMessage = "El apellido es requerido.")]
        [MinLength(2, ErrorMessage = "El apellido no puede contener menos de 2 letras")]
        public string pApellido1 { get; set; } = string.Empty;
        [DisplayName("Cédula")]
        [Required(ErrorMessage = "La cédula es requerido.")]
        [StringLength(9, ErrorMessage = "La cédula debe tener 9 dígitos", MinimumLength = 9)]
        public string pCedula { get; set; } = string.Empty;
        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(8, ErrorMessage = "El número de teléfono debe contener 8 dígitos", MinimumLength = 8)]
        public string pTelefono { get; set; } = string.Empty;
        [DisplayName("Correo Eléctronico")]
        [Required(ErrorMessage = "El correo es requerido.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Digíte un correo válido")]
        public string pCorreo { get; set; } = string.Empty;
        [DisplayName("Contraseña")]
        [Required(ErrorMessage = "La contraseña es requerida")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$",
        ErrorMessage = "La contraseña debe contener al menos 8 caracteres, una letra mayúscula, una letra minúscula, un número y un carácter especial")]
        public string pContrasena { get; set; } = string.Empty;
        [DisplayName("Rol")]
        public int pIdRol { get;set; }
        [DisplayName ("Nombre Completo")]
        public string NombreCompletoUsuario { get; set; } = string.Empty;
        [DisplayName("Rol")]
        public string NombreRol { get; set; } = string.Empty;
        public bool Estado { get; set; } 
        public class RespuestaUsuario
        {
            public int Codigo { get; set; }
            public string Mensaje { get; set; } = string.Empty;
            public List<UsuariosEntities> RespuestaUsuarios { get; set; } = new List<UsuariosEntities>();
        }

    }
}
