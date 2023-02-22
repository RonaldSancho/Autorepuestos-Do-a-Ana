using Autorepuestos.Entities;
using static Autorepuestos.Entities.CatalogosEntities;


namespace Autorepuestos.Interfaces
{
    public interface ICatalogosModel
    {
        public CatalogosEntities? VerProductoCatalogo(int id);
        public List<CatalogosEntities> VerCatalogos(CatalogosEntities catalogo);


    }
}
