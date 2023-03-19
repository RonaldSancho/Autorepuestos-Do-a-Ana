using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface IFacturaModel
    {
        public void CrearFactura(int? IdUsuario);
        public void EliminarFactura(int IdFactura);
        public List<FacturaEntities> VerFacturas();
        public FacturaEntities? VerDetalleFactura(int IdFactura);
    }
}
