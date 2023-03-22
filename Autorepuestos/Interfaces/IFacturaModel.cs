using Autorepuestos.Entities;
using static Autorepuestos.Entities.FacturaEntities;

namespace Autorepuestos.Interfaces
{
    public interface IFacturaModel
    {
        public void EliminarFactura(int IdFactura);
        public List<FacturaEntities> VerFacturas();
        public FacturaEntities? VerDetalleFactura(int IdFactura);
        public RespuestaFactura ConsultarTipoPago();
        public void CrearFactura(FacturaEntities entidad, int? IdUsuario);
    }
}
