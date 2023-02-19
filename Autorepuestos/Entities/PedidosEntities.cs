using System.ComponentModel;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class PedidosEntities
    {
        public int pIdPedido { get; set; }
        [DisplayName("Producto")]
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        [DisplayName("Proveedor")]
        public int IdProveedor { get; set; }
        [DisplayName("Precio Producto")]
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
