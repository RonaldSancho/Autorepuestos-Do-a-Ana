using static Mysqlx.Expect.Open.Types.Condition.Types;
namespace Autorepuestos.Entities
{
    public class PedidosEntities
    {
        
            public int IdPedido { get; set; }
            public int IdProducto { get; set; }
            public int IdFactura { get; set; }
            public int Cantidad { get; set; }
            public int IdProveedor { get; set; }
            public int PrecioProducto { get; set; }
            public int PrecioTotal { get; set; }
            public int IdUsuario { get; set; }

        
    }
}
