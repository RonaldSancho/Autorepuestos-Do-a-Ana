﻿using Autorepuestos.Entities;
using static Autorepuestos.Entities.ProductosEntities;
namespace Autorepuestos.Interfaces
{
    public interface IProductosModel
    {
        public void CrearProducto(ProductosEntities producto);
        public void EditarProducto(ProductosEntities producto);
        public void EliminarProducto(int id); 
        public ProductosEntities? VerProducto(int id);
        public List<ProductosEntities> VerProductos(ProductosEntities producto); 
        public void DevolucionProducto(ProductosEntities entidad);
    }
}