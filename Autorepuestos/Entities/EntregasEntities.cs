using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class EntregasEntities
    {
        public int pIdEntrega { get; set; }

        //[DisplayName("Producto")]
        //public int IdProducto { get; set; }

        //[DisplayName("Producto")]
        //public string NombreProducto { get; set; } = string.Empty;


        [DisplayName("Productos a Entregar")]
        [Required(ErrorMessage = "El o los productos a entregar son requerido.")]
        [MinLength(3, ErrorMessage = "Este espacio no puede contener menos de 3 letras")]
        public string Productos { get; set; } = string.Empty;

        [DisplayName("Direccion")]
        [Required(ErrorMessage = "La dirección es requerida.")]
        [MinLength(15, ErrorMessage = "La dirección debe ser específica por lo que debe tener más de 15 caracteres")]
        public string DireccionEntrega { get; set; } = string.Empty;
        
        [DisplayName("Cliente")]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
       
        [DisplayName("Estado Entrega")]
        public string Estado { get; set; } = string.Empty;

        [DisplayName("Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [DisplayName("Cédula")]
        public string Cedula { get; set; } = string.Empty;
        [DisplayName("Cliente")]
        public string pNombre { get; set; } = string.Empty;

        public class RespuestaEntrega
        {
            public int cod { get; set; }
            public string message { get; set; } = string.Empty;
            public List<EntregasEntities> RespuestaEntregas { get; set; } = new List<EntregasEntities>();
        }
    }
}
