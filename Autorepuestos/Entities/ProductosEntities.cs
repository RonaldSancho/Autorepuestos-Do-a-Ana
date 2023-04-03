using System.ComponentModel;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class ProductosEntities
    {
        public int pIdProducto { get; set; }
        [DisplayName("Producto")]
        public string NombreProducto { get; set; } = string.Empty;
        [DisplayName("Descripcion")]
        public string Descripcion { get; set; } = string.Empty;
        [DisplayName("Precio")]
        public decimal Precio { get; set; }
        [DisplayName("Categoria")]
        public int IdCategoria { get; set; }
        [DisplayName("Existencias")]
        public int Existencias { get; set; }
        [DisplayName("Proveedor")]
        public int IdProveedor { get; set; }
        public string Imagen { get; set; } = string.Empty;
        public string ImagenN { get; set; } = string.Empty;
        public IFormFile ImagenDatos { get; set; } 
        public string NombreCategoria { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty; public class RespuestaProducto
        {
            public int Codigo { get; set; }
            public string Mensaje { get; set; } = string.Empty;
            public List<ProductosEntities> RespuestaProductos { get; set; } = new List<ProductosEntities>();
        }
    }
}