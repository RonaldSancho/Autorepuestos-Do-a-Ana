using Autorepuestos.Interfaces;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using Autorepuestos.Entities;
using static Autorepuestos.Entities.FacturaEntities;
using static Autorepuestos.Entities.EntregasEntities;

namespace Autorepuestos.Models
{
    public class FacturaModel : IFacturaModel
    {
        private readonly IConfiguration _configuration;


        public FacturaModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void EliminarFactura(int IdFactura)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("EliminarFactura", new { IdFactura }, commandType: CommandType.StoredProcedure);
            }

        }

        public FacturaEntities? VerDetalleFactura(int IdFactura)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<FacturaEntities>("VerDetalleFactura", new { IdFactura }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
        }

        public RespuestaFactura ConsultarTipoPago()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultarTipoPago", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaFactura respuesta = new RespuestaFactura();
                List<FacturaEntities> datos = new List<FacturaEntities>();
                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new FacturaEntities
                        {
                            IdTipoPago = item.IdTipoPago,
                            TipoPago = item.TipoPago,
                        });
                    }
                    respuesta.RespuestaFacturas = datos;
                }
                return respuesta;
            }
        }
        public RespuestaFactura ConsultarTipoRetiro()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query("ConsultarTipoRetiro", new { }, commandType: CommandType.StoredProcedure).ToList();
                RespuestaFactura respuesta = new RespuestaFactura();
                List<FacturaEntities> datos = new List<FacturaEntities>();
                if (resultado.Count > 0)
                {
                    foreach (var item in resultado)
                    {
                        datos.Add(new FacturaEntities
                        {
                            IdTipoRetiro = item.IdTipoRetiro,
                            TipoRetiro = item.TipoRetiro,
                        });
                    }
                    respuesta.RespuestaFacturas = datos;
                }
                return respuesta;
            }
        }
        public void CrearFactura(FacturaEntities entidad, int? IdUsuario)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CrearFactura", new {entidad.IdTipoPago, entidad.IdTipoRetiro, IdUsuario }, commandType: CommandType.StoredProcedure);
            }
        }
        public List<FacturaEntities> VerFacturas()
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                return conexion.Query<FacturaEntities>("VerFacturas", new { }, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public void CambiarEstadoFactura(int IdFactura)
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                conexion.Execute("CambiarEstadoFactura", new { IdFactura }, commandType: CommandType.StoredProcedure);
            }

        }

        public FacturaEntities? EnviarCorreoConfirmacion(FacturaEntities entidad )
        {
            using (var conexion = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            {
                var resultado = conexion.Query<FacturaEntities>
                    ("EnviarCorreoConfirmacion", new { entidad.IdUsuario,entidad.IdTipoPago,entidad.IdTipoRetiro }, 
                    commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                return resultado;
            }
        }
    }
}
