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

        /*aqui empieza la parte del detalle como tal para verificar la compra*/
        public void CreandoDetalle(int? IdUsuario);
        public List<CarritoEntities> ConsultarDetalle(int? IdUsuario);
    }
}
