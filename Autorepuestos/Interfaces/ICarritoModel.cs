using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface ICarritoModel
    {
        public List<CarritoEntities> ConsultarCarrito(CarritoEntities carro);

        public CarritoEntities? EliminarCarrito(int id);

        public void AgregarCarrito(int id, int cant);

    }
}
