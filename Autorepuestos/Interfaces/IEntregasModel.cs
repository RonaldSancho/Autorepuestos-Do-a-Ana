using Autorepuestos.Entities;
using static Autorepuestos.Entities.EntregasEntities;

namespace Autorepuestos.Interfaces
{
    public interface IEntregasModel
    {
        public List<EntregasEntities> VerEntregas(EntregasEntities entrega);
        public void AgregarEntrega(EntregasEntities entrega);
        public void Editar(EntregasEntities entrega);
        public void EliminarEntrega(int id);
        public EntregasEntities? ConsultarEntrega(int id);
        public RespuestaEntrega ConsultaEntregaProducto();
        public RespuestaEntrega ConsultaEntregaUsuario();
    }
}
