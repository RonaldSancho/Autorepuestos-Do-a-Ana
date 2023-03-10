using System.ComponentModel;

namespace Autorepuestos.Entities
{
    public class CarritoEntities
    {

        public int IdCarrito { get; set; }
        public int IdProducto { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        [DisplayName("Producto")]
        public string NombreProducto { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;

        public string Existencias { get; set; } = string.Empty;

        public int IdUsuario { get; set; }

        public class RespuestaCarrito
        {
            public List<CarritoEntities> RespuestaCarro { get; set; } = new List<CarritoEntities>();

            public CarritoEntities? Carrito { get; set; } 
        }
    }
}