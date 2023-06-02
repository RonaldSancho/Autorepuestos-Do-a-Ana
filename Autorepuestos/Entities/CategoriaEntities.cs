using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Autorepuestos.Entities
{
    public class CategoriaEntities
    {
        public int IdCategoria { get; set; }
        [DisplayName("Categoría")]
        [Required(ErrorMessage = "El nombre de la categoría es requerido.")]
        [MinLength(2, ErrorMessage = "La categoría no puede contener menos de 2 letras")] 
        public string NombreCategoria { get; set; } = string.Empty;
    }
    public class RespuestaCategoria
    {
        public int Codigo { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<CategoriaEntities> RespuestaCategorias { get; set; } = new List<CategoriaEntities>();
    }
}
