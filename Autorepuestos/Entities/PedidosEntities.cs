using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class PedidosEntities
    {
        public int pIdPedido { get; set; }
        [DisplayName("Producto")]
        public int IdProducto { get; set; }
        [DisplayName("Cantidad")]
        [Required(ErrorMessage = "La cantidad de existencias es requerida.")]
        [Range(1, 100, ErrorMessage = "La cantidad de existencias debe estar entre 1 y 100.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La cantidad de existencias debe ser un número.")]
        public int? Cantidad { get; set; }
        [DisplayName("Proveedor")]
        public int IdProveedor { get; set; }
        [DisplayName("Precio Producto")]
        [Required(ErrorMessage = "El precio es requerido.")]
        [Range(100, 999999, ErrorMessage = "El precio debe estar entre 100 y 999999.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El precio debe ser un número.")]
        public int PrecioProducto { get; set; }
        [DisplayName("Precio Total")]
        public int PrecioTotal { get; set; }
        [DisplayName("Colaborador")]
        public int IdUsuario { get; set; }

        public string NombreProducto { get; set; } = string.Empty;
        public string NombreUsuario { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;

        public class RespuestaPedido
        {
            public int Codigo { get; set; }
            public string Mensaje { get; set; } = string.Empty;
            public List<PedidosEntities> RespuestaPedidos { get; set; } = new List<PedidosEntities>();
        }



    }
}
