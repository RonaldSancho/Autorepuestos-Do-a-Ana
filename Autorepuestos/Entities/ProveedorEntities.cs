using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autorepuestos.Entities
{
    public class ProveedorEntities
    {
        public int IdProveedor { get; set; }
        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "El nombre del proveedor es requerido.")]
        [MinLength(2, ErrorMessage = "El nombre no puede contener menos de 2 letras")]
        public string NombreProveedor { get; set; } = string.Empty;
        [DisplayName("Teléfono")]
        [Required(ErrorMessage = "El número de teléfono es requerido.")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(8, ErrorMessage = "El número de teléfono debe contener 8 dígitos", MinimumLength = 8)]
        public string Telefono { get; set; } = string.Empty;
        [DisplayName("Dirección")]
        [Required(ErrorMessage = "La dirección es requerida.")]
        [MinLength(10, ErrorMessage = "La dirección no puede contener menos de 10 letras")]
        public string Direccion { get; set; } = string.Empty;

    }
    public class RespuestaProveedor
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<ProveedorEntities> RespuestaProveedores { get; set; } = new List<ProveedorEntities>();
    }
}
