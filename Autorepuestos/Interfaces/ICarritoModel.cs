using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface ICarritoModel
    {
        public List<CarritoEntities> ConsultarCarrito(CarritoEntities carro);

        public CarritoEntities? EliminarCarrito(int id);

        public void AgregarCarrito(CatalogosEntities entidad);
        public CarritoEntities? MostrarProductoCarrito(int id);
        public void EditarCarrito(CarritoEntities entidad);

        public void FinalizarCompra();

        public CatalogosEntities? ProductoCarrito(int id);

        public CarritoEntities? ConsultaExisteProductoCarrito(int id);

    }
}
