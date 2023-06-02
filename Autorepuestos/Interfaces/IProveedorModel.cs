using Autorepuestos.Entities;
using static Autorepuestos.Entities.PedidosEntities;
using static Autorepuestos.Entities.ProductosEntities;

namespace Autorepuestos.Interfaces
{
    public interface IProveedorModel
    {
        public void Crear(ProveedorEntities proveedor);
        public void Editar(ProveedorEntities proveedor);
        public ProveedorEntities? VerProveedor(int id);
        public List<ProveedorEntities> VerProveedores(ProveedorEntities proveedor);
        public RespuestaProveedor ConsultaProductoProveedor();
        public RespuestaProveedor ConsultaPedidoProveedor();
    }
}
