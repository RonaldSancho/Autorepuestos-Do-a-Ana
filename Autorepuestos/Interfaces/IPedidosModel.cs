using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface IPedidosModel
    {
        public PedidosEntities Crear(PedidosEntities pedido);
        public void Editar(PedidosEntities pedido);
        public void eliminar(int id);
        public PedidosEntities verPedido(int id);
        public List<PedidosEntities> VerPedidos(PedidosEntities pedido);
    }
}
