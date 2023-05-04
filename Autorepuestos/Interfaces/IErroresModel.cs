using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface IErroresModel
    {
        public void RegistrarErrores(ErroresEntities errores);
        public void RegistrarBitacora(ErroresEntities errores);
    }
}
