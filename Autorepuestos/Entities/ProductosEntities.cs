using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class ProductosEntities : ValidationAttribute
    {
        [DisplayName("Producto")]
        public int pIdProducto { get; set; }

        [DisplayName("Producto")]
        [Required(ErrorMessage = "El nombre del producto es requerido.")]
        [MinLength(3, ErrorMessage = "El producto no puede contener menos de 3 letras.")]
        public string NombreProducto { get; set; } = string.Empty;

        [DisplayName("Descripción")]
        [Required(ErrorMessage = "La descripción del producto es requerida.")]
        [MinLength(5, ErrorMessage = "La descripción no puede contener menos de 5 letras.")]
        public string Descripcion { get; set; } = string.Empty;

        [DisplayName("Precio")]
        [Required(ErrorMessage = "El precio del producto es requerido.")]
        [Range(100, 999999, ErrorMessage = "El precio debe estar entre 100 y 999999.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El precio debe ser un número.")]
        public decimal? Precio { get; set; }


        [DisplayName("Categoría")]
        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int IdCategoria { get; set; }

        [DisplayName("Existencias")]
        [Required(ErrorMessage = "La cantidad de existencias es requerida.")]
        [Range(1, 100, ErrorMessage = "La cantidad de existencias debe estar entre 1 y 100.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cantidad de existencias debe ser un número.")]
        public int? Existencias { get; set; }

        [DisplayName("Proveedor")]
        [Required(ErrorMessage = "Debe seleccionar un proveedor.")]
        public int IdProveedor { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string ImagenN { get; set; } = string.Empty;

        [DisplayName("Imagen")]
        public IFormFile ImagenDatos { get; set; } 
        public string NombreCategoria { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        [DisplayName("Cantidad")]
        public int Cantidad { get; set; }
        public class RespuestaProducto
        {
            public int Codigo { get; set; }
            public string Mensaje { get; set; } = string.Empty;
            public List<ProductosEntities> RespuestaProductos { get; set; } = new List<ProductosEntities>();
        }
    }
}