namespace Autorepuestos.Entities
{
    public class ErroresEntities
    {
        public string Mensaje { get; set; } = string.Empty;
        public string Origen { get; set;} = string.Empty;
        public int? IdUsuario { get; set; }

        //estos dos atributos son para la parte de bitacoras
        public string Accion { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
    }
}
