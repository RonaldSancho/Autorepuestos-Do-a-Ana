using System.ComponentModel;
using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class EntregasEntities
    {
        public int pIdEntrega { get; set; }
        public int IdProducto { get; set; }
        [DisplayName("Producto")]
        public string NombreProducto { get; set; } = string.Empty;
        [DisplayName("Dirección")]
        public string DireccionEntrega { get; set; } = string.Empty;
        [DisplayName("Cliente")]
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        [DisplayName("Estado Entrega")]
        public string Estado { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Cedula { get; set; } = string.Empty;

        public class RespuestaEntrega
        {
            public int cod { get; set; }
            public string message { get; set; } = string.Empty;
            public List<EntregasEntities> RespuestaEntregas { get; set; } = new List<EntregasEntities>();
        }
    }
}
