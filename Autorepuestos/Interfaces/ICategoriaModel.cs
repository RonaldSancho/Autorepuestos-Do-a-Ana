using Autorepuestos.Entities;
using static Autorepuestos.Entities.ProductosEntities;

namespace Autorepuestos.Interfaces
{
    public interface ICategoriaModel
    {
        public void Crear(CategoriaEntities categoria);
        public void Editar(CategoriaEntities categoria);
        public CategoriaEntities? VerCategoria(int id);
        public List<CategoriaEntities> VerCategorias(CategoriaEntities categoria);
        public RespuestaCategoria ConsultaProductoCategoria();
    }
}
