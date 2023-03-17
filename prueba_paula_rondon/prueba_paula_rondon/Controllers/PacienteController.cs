using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System;
using prueba_paula_rondon.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace prueba_paula_rondon.Controllers
{
    public class PacienteController : Controller
    {
        private readonly IConfiguration configuracion;

        public PacienteController(IConfiguration configuracion_)
        {
            configuracion = configuracion_;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            using var httpClient = new HttpClient();
            List<PacienteModel> patientModels = new List<PacienteModel>();
            var url = "https://localhost:44324/api/consultarPacientes";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using (var response = await httpClient.SendAsync(request))
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseObjeto = JsonConvert.DeserializeObject<RtaList>(jsonResponse);
                patientModels = responseObjeto.data;
            }

            ViewBag.consulta = patientModels;

            return View();
        }


        public async Task<IActionResult> Create()
        {
            using var httpClient = new HttpClient();
            List<TipoDocumentoModel> documentModels = new List<TipoDocumentoModel>();
            var url = "https://localhost:44324/api/consultarTipoDocumentos";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            using (var response = await httpClient.SendAsync(request))
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var responseObjeto = JsonConvert.DeserializeObject<RtaDocument>(jsonResponse);
                documentModels = responseObjeto.data;
            }

            ViewBag.documento = documentModels;

            return View();
        }

        public async Task<IActionResult> Update(int? idPaciente)
        {
            if (idPaciente == null)
            {
                return NotFound();
            }
            else
            {
                using var httpClient = new HttpClient();
                PacienteModel patientModels = new PacienteModel();
                var url = "https://localhost:44324/api/consultarPaciente/" + idPaciente;
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                using (var response = await httpClient.SendAsync(request))
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObjeto = JsonConvert.DeserializeObject<Rta>(jsonResponse);
                    patientModels = responseObjeto.data;
                }

                using var httpClient1 = new HttpClient();
                List<TipoDocumentoModel> documentModels = new List<TipoDocumentoModel>();
                var url1 = "https://localhost:44324/api/consultarTipoDocumentos";
                var request1 = new HttpRequestMessage(HttpMethod.Get, url1);

                using (var response = await httpClient1.SendAsync(request1))
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObjeto = JsonConvert.DeserializeObject<RtaDocument>(jsonResponse);
                    documentModels = responseObjeto.data;
                }

                ViewBag.documento = documentModels;

                return View(patientModels);
            }
        }

        public async Task<IActionResult> Delete(int? idPaciente)
        {
            if (idPaciente == null)
            {
                return NotFound();
            }
            else
            {
                using var httpClient = new HttpClient();
                List<TipoDocumentoModel> documentModels = new List<TipoDocumentoModel>();
                var url = "https://localhost:44324/api/consultarTipoDocumentos";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                using (var response = await httpClient.SendAsync(request))
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObjeto = JsonConvert.DeserializeObject<RtaDocument>(jsonResponse);
                    documentModels = responseObjeto.data;
                }

                ViewBag.documento = documentModels;


                using var httpClient1 = new HttpClient();
                PacienteModel patientModels = new PacienteModel();
                var url1 = "https://localhost:44324/api/consultarPaciente/" + idPaciente;
                var request1 = new HttpRequestMessage(HttpMethod.Get, url1);

                using (var response = await httpClient1.SendAsync(request1))
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var responseObjeto = JsonConvert.DeserializeObject<Rta>(jsonResponse);
                    patientModels = responseObjeto.data;

                }
                return View(patientModels);
            }
        }


        // Metodo para crear un paciente
        #region Create
        [HttpPost]
        public async Task<IActionResult> Create(PacienteModel data)
        {
            // Llamado de API
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44324/");
                var jsonData = JsonConvert.SerializeObject(data);
                var contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseCliente = await httpClient.PostAsync($"api/crearEditarPaciente", contentBody);

                if (!responseCliente.IsSuccessStatusCode)
                {
                    return BadRequest(responseCliente);
                }

                TempData["mensaje"] = "El paciente ha sido creado con exito";
                return RedirectToAction("Index");

            }
        }
        #endregion

        // Metodo para actualizar un paciente
        #region ActualizarPaciente
        [HttpPost]
        public async Task<IActionResult> Update(PacienteModel data)
        {
            // Llamado de API
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44324/");
                var jsonData = JsonConvert.SerializeObject(data);
                var contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseCliente = await httpClient.PostAsync($"api/crearEditarPaciente", contentBody);

                if (!responseCliente.IsSuccessStatusCode)
                {
                    return BadRequest(responseCliente);
                }

                TempData["mensaje"] = "El paciente ha sido actualizado con exito";
                return RedirectToAction("Index");

            }
        }
        #endregion


        //Eliminar paciente
        #region DeletePatient
        [HttpPost]
        public async Task<IActionResult> DeletePatient(int idPaciente)
        {
            // Llamado de API
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri("https://localhost:44324/");
                var jsonData = JsonConvert.SerializeObject(idPaciente);
                var contentBody = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseCliente = await httpClient.DeleteAsync($"api/eliminarPaciente/" + idPaciente);

                if (!responseCliente.IsSuccessStatusCode)
                {
                    return BadRequest(responseCliente);
                }

                TempData["mensaje"] = "El paciente ha sido eliminado";
                return RedirectToAction("Index");

            }
        }
        #endregion
    }
}
