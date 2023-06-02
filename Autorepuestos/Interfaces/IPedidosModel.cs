using Autorepuestos.Entities;
using static Autorepuestos.Entities.PedidosEntities;

namespace Autorepuestos.Interfaces
{
    public interface IPedidosModel
    {
        public void Crear(PedidosEntities pedido);
        public void Editar(PedidosEntities pedido);
        public void eliminar(int id);
        public PedidosEntities? verPedido(int id);
        public List<PedidosEntities> VerPedidos(PedidosEntities pedido);
        public RespuestaPedido ConsultaPedidoProducto();
        public RespuestaPedido ConsultaPedidoUsuario();
    }
}
