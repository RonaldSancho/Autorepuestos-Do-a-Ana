using Google.Protobuf.WellKnownTypes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autorepuestos.Entities
{
    public class FacturaEntities
    {
        [DisplayName("Número Factura")]
        public int IdFactura { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; } 
        public int IdUsuario { get; set;}
        [DisplayName("Monto Total")]
        public decimal MontoTotal { get; set;}
        [DisplayName("Teléfono")]
        public string Telefono { get; set;} = string.Empty;
        [DisplayName("Dirección")] 
        public string Direccion { get; set; } = string.Empty;
        [DisplayName("Nombre Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

    }
}
