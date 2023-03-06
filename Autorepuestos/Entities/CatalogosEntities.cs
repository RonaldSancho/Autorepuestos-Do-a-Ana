using System.ComponentModel;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class CatalogosEntities
    {
        public int IdProducto { get; set; }
        public decimal Precio { get; set; }
        public int IdCategoria { get; set; }
        public int Existencias { get; set; }
        public int IdProveedor { get; set; }
        public string Imagen { get; set; } = string.Empty;
        [DisplayName("Producto")]
        public string NombreProducto { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public int Cantidad { get; set; }
    }

    public class RespuestaCatalogo
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<CatalogosEntities> RespuestaCatalogos { get; set; } = new List<CatalogosEntities>();
    }
}

