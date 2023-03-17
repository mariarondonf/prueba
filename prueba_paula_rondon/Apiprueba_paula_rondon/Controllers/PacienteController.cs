using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using prueba_paula_rondon.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace Apiprueba_paula_rondon.Controllers
{
    [Route("api")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly IConfiguration configuracion;

        private Rta rta = new Rta();

        public PacienteController(IConfiguration configuracion_)
        {
            configuracion = configuracion_;
        }

        // Metodo GET para consultar documentos
        #region consultarTipoDocumento
        // GET: api/consultarTipoDocumentos
        /// <summary>
        /// Obtener tipos de documento
        /// </summary>
        /// <remarks>
        /// Método para obtener tipos de documento
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        /// <response code="400">Bad Request. url no encontrada</response>
        /// <response code="200">OK. arqueo retornado exitosamente</response>
        [HttpGet]
        [Route("consultarTipoDocumentos")]
        public async Task<IActionResult> consultarTipoDocumento()
        {
            using (SqlConnection connection = new SqlConnection(configuracion.GetConnectionString("bdconnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    var consulta = "Select * from tiposDocumento";

                    SqlCommand cmd = new SqlCommand(consulta, connection);

                    cmd.CommandType = CommandType.Text;

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();

                    List<TipoDocumentoModel> consultList = new List<TipoDocumentoModel>();

                    while (await dataReader.ReadAsync())
                    {
                        TipoDocumentoModel consultList2 = new TipoDocumentoModel();

                        if (dataReader["idTipoDocumento"] != DBNull.Value) { consultList2.idTipoDocumento = Convert.ToInt32(dataReader["idTipoDocumento"]); }
                        if (dataReader["nombreDocumento"] != DBNull.Value) { consultList2.nombreDocumento = Convert.ToString(dataReader["nombreDocumento"]); }


                        consultList.Add(consultList2);
                    }

                    await dataReader.CloseAsync();

                    rta.ok = true;
                    rta.data = consultList;

                    return Ok(rta);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error");
                }

            }
        }
        #endregion


        // Metodo GET para consultar pacientes
        #region consultarPacientes
        // GET: api/consultarPacientes
        /// <summary>
        /// Obtener pacientes
        /// </summary>
        /// <remarks>
        /// Método para obtener pacientes
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        /// <response code="400">Bad Request. url no encontrada</response>
        /// <response code="200">OK. arqueo retornado exitosamente</response>
        [HttpGet]
        [Route("consultarPacientes")]
        public async Task<IActionResult> consultarPacientes()
        {
            using (SqlConnection connection = new SqlConnection(configuracion.GetConnectionString("bdconnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_consultarPacientes", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();

                    List<PacienteModel> consultList = new List<PacienteModel>();

                    while (await dataReader.ReadAsync())
                    {
                        PacienteModel consultList2 = new PacienteModel();

                        if (dataReader["idPaciente"] != DBNull.Value) { consultList2.idPaciente = Convert.ToInt32(dataReader["idPaciente"]); }
                        if (dataReader["numeroDocumento"] != DBNull.Value) { consultList2.numeroDocumento = Convert.ToString(dataReader["numeroDocumento"]); }
                        if (dataReader["nombres"] != DBNull.Value) { consultList2.nombres = Convert.ToString(dataReader["nombres"]); }
                        if (dataReader["apellidos"] != DBNull.Value) { consultList2.apellidos = Convert.ToString(dataReader["apellidos"]); }
                        if (dataReader["correoElectronico"] != DBNull.Value) { consultList2.correoElectronico = Convert.ToString(dataReader["correoElectronico"]); }
                        if (dataReader["telefono"] != DBNull.Value) { consultList2.telefono = Convert.ToString(dataReader["telefono"]); }
                        if (dataReader["fechaNacimiento"] != DBNull.Value) { consultList2.fechaNacimiento = Convert.ToDateTime(dataReader["fechaNacimiento"]); }
                        if (dataReader["estadoAfiliacion"] != DBNull.Value) { consultList2.estadoAfiliacion = Convert.ToBoolean(dataReader["estadoAfiliacion"]); }
                        if (dataReader["idTipoDocumento"] != DBNull.Value) { consultList2.idTipoDocumento = Convert.ToInt32(dataReader["idTipoDocumento"]); }

                        consultList.Add(consultList2);
                    }

                    await dataReader.CloseAsync();

                    rta.ok = true;
                    rta.data = consultList;

                    return Ok(rta);
                }
                catch (Exception ex)
                {
                    return BadRequest("Error");
                }

            }
        }
        #endregion


        // Metodo GET para consultar paciente mediante un id
        #region consultarPaciente
        // GET: api/consultarPaciente/{idPaciente}
        /// <summary>
        /// Obtener paciente
        /// </summary>
        /// <remarks>
        /// Método para obtener paciente
        /// </remarks>
        /// <param name="idPaciente">Identificador paciente</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        /// <response code="400">Bad Request. url no encontrada</response>
        /// <response code="200">OK. arqueo retornado exitosamente</response>
        [HttpGet]
        [Route("consultarPaciente/{idPaciente}")]
        public async Task<IActionResult> consultarPaciente([FromRoute] int idPaciente)
        {
            using (SqlConnection connection = new SqlConnection(configuracion.GetConnectionString("bdconnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_consultarPaciente", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPaciente", idPaciente));

                    SqlDataReader dataReader = await cmd.ExecuteReaderAsync();

                    PacienteModel consultList2 = new PacienteModel();

                    while (await dataReader.ReadAsync())
                    {
                        if (dataReader["idPaciente"] != DBNull.Value) { consultList2.idPaciente = Convert.ToInt32(dataReader["idPaciente"]); }
                        if (dataReader["numeroDocumento"] != DBNull.Value) { consultList2.numeroDocumento = Convert.ToString(dataReader["numeroDocumento"]); }
                        if (dataReader["nombres"] != DBNull.Value) { consultList2.nombres = Convert.ToString(dataReader["nombres"]); }
                        if (dataReader["apellidos"] != DBNull.Value) { consultList2.apellidos = Convert.ToString(dataReader["apellidos"]); }
                        if (dataReader["correoElectronico"] != DBNull.Value) { consultList2.correoElectronico = Convert.ToString(dataReader["correoElectronico"]); }
                        if (dataReader["telefono"] != DBNull.Value) { consultList2.telefono = Convert.ToString(dataReader["telefono"]); }
                        if (dataReader["fechaNacimiento"] != DBNull.Value) { consultList2.fechaNacimiento = Convert.ToDateTime(dataReader["fechaNacimiento"]); }
                        if (dataReader["estadoAfiliacion"] != DBNull.Value) { consultList2.estadoAfiliacion = Convert.ToBoolean(dataReader["estadoAfiliacion"]); }
                        if (dataReader["idTipoDocumento"] != DBNull.Value) { consultList2.idTipoDocumento = Convert.ToInt32(dataReader["idTipoDocumento"]); }
                    }

                    await dataReader.CloseAsync();

                    rta.ok = true;
                    rta.data = consultList2;

                    return Ok(rta);
                }
                catch (Exception ex)
                {
                    PacienteModel consultList = new PacienteModel();
                    return BadRequest("Error");
                }

            }
        }
        #endregion


        // Metodo POST para registrar/actualizar paciente
        #region crearEditarPaciente
        // POST: api/crearEditarPaciente
        /// <summary>
        /// Crear o actualizar pacientes
        /// </summary>
        /// <remarks>
        /// Método para crear o actualizar pacientes
        /// </remarks>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        /// <response code="400">Bad Request. url no encontrada</response>
        /// <response code="200">OK. arqueo retornado exitosamente</response>
        [HttpPost]
        [Route("crearEditarPaciente")]
        public async Task<IActionResult> crearEditarPaciente([FromBody] PacienteModel data)
        {
            using (SqlConnection connection = new SqlConnection(configuracion.GetConnectionString("bdconnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_crearEditarPaciente", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPaciente", data.idPaciente));
                    cmd.Parameters.Add(new SqlParameter("@numeroDocumento", data.numeroDocumento));
                    cmd.Parameters.Add(new SqlParameter("@nombres", data.nombres));
                    cmd.Parameters.Add(new SqlParameter("@apellidos", data.apellidos));
                    cmd.Parameters.Add(new SqlParameter("@correoElectronico", data.correoElectronico));
                    cmd.Parameters.Add(new SqlParameter("@telefono", data.telefono));
                    cmd.Parameters.Add(new SqlParameter("@fechaNacimiento", data.fechaNacimiento));
                    cmd.Parameters.Add(new SqlParameter("@estadoAfiliacion", data.estadoAfiliacion));
                    cmd.Parameters.Add(new SqlParameter("@idTipoDocumento", Convert.ToInt32(data.idTipoDocumento)));

                    await cmd.ExecuteNonQueryAsync();

                    rta.ok = true;

                    return Ok(rta);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }

            }
        }
        #endregion

        // Metodo DELETE para eliminar paciente
        #region eliminarPaciente/{idPaciente}
        // DELETE: api/eliminarPaciente
        /// <summary>
        /// Crear o actualizar pacientes
        /// </summary>
        /// <remarks>
        /// Método para crear o actualizar pacientes
        /// </remarks>
        /// <param name="idPaciente">Identificador paciente</param>
        /// <response code="401">Unauthorized. No se ha indicado o es incorrecto el token JWT de acceso</response>
        /// <response code="400">Bad Request. url no encontrada</response>
        /// <response code="200">OK. arqueo retornado exitosamente</response>
        [HttpDelete]
        [Route("eliminarPaciente/{idPaciente}")]
        public async Task<IActionResult> eliminarPaciente([FromRoute] int idPaciente)
        {
            using (SqlConnection connection = new SqlConnection(configuracion.GetConnectionString("bdconnection")))
            {
                try
                {
                    await connection.OpenAsync();

                    SqlCommand cmd = new SqlCommand("sp_eliminarPaciente", connection);

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add(new SqlParameter("@idPaciente", idPaciente));

                    await cmd.ExecuteNonQueryAsync();

                    rta.ok = true;

                    return Ok(rta);
                }
                catch (Exception ex)
                {

                    return BadRequest(ex);
                }

            }
        }
        #endregion
    }
}
