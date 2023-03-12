using Autorepuestos.Entities;

namespace Autorepuestos.Interfaces
{
    public interface ICarritoModel
    {
        public List<CarritoEntities> ConsultarCarrito(CarritoEntities carro, int? IdUsuario);

        public CarritoEntities? EliminarCarrito(int id);

        public void AgregarCarrito(CatalogosEntities entidad, int? IdUsuario);
        public CarritoEntities? MostrarProductoCarrito(int id);
        public void EditarCarrito(CarritoEntities entidad);

        public CatalogosEntities? ProductoCarrito(int id);

        public CarritoEntities? ConsultaExisteProductoCarrito(int id, int? IdUsuario);
        public void CrearDetalleCarrito(int? IdUsuario);
    }
}
