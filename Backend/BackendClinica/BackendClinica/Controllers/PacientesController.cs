using Core.Modelos.Entorno;
using Core.Servicios.Impl;
using Core.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendClinica.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PacientesController : Controller
    {

        private Configuracion conf;
        public PacientesController(Configuracion conf)
        {
            this.conf = conf;
        }
        [HttpGet("ObtenerNotas/{idPaciente}")]
        public async Task<ActionResult> ObtenerNotas(string idPaciente)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.ObtenerNotas(idPaciente);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("ActualizaNotas")]
        public async Task<ActionResult> ActualizaNotas([FromBody] InfoNotas info)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.ActualizaNotas(info);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("Insertar")]
        public async Task<ActionResult> CrearPaciente([FromBody]PacienteModelo paciente)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.CrearPaciente(paciente);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("Editar")]
        public async Task<ActionResult> EditarPaciente([FromBody] PacienteModelo paciente)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {
                var response = await servicio.EditarPaciente(paciente);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //[Authorize]
        [HttpGet("Pacientes/{idPaciente}")]
        public async Task<ActionResult> ObtenerPaciente(string idPaciente)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.ObtenerPaciente(idPaciente);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpGet("Eliminar/{idPaciente}")]
        public async Task<ActionResult> EliminarUsuario(string idPaciente)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.EliminarPacientte(idPaciente);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("ObtenerPacientes")]
        public async Task<ActionResult> ObtenerPacientes([FromBody]CriterioPacienteModelo data)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.ObtenerPacientes(data);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("InsertarHistorial")]
        public async Task<ActionResult> CrearHistorial([FromBody]PacienteModelo paciente)
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.CrearHistorial(paciente);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //[Authorize]
        [HttpGet("CargaMasiva")]
        public async Task<ActionResult> CargaMasiva()
        {
            IPaciente servicio = new Paciente(this.conf);
            try
            {

                var response = await servicio.CargaMasiva();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
