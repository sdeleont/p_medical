using Core.Modelos.Entorno;
using Core.Servicios.Impl;
using Core.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendClinica.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CitaController : Controller
    {
        private Configuracion conf;
        public CitaController(Configuracion conf)
        {
            this.conf = conf;
        }
        [Authorize]
        [HttpPost("Crear")]
        public async Task<ActionResult> CrearCita([FromBody] CitaModelo cita)
        {
            ICita servicio = new Cita(this.conf);
            try
            {
                var response = await servicio.CrearCita(cita);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("Eliminar")]
        public async Task<ActionResult> EliminarCita([FromBody] CitaModelo cita)
        {
            ICita servicio = new Cita(this.conf);
            try
            {
                var response = await servicio.EliminarCita(cita);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("Modificar")]
        public async Task<ActionResult> ModificarCita([FromBody] CitaModelo cita)
        {
            ICita servicio = new Cita(this.conf);
            try
            {
                var response = await servicio.ModificarCita(cita);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("ObtenerCalendario/{usuario}/{month}/{year}")]
        public async Task<ActionResult> ObtenerCalendario(string usuario, string month, string year)
        {
            ICita servicio = new Cita(this.conf);
            try
            {
                var response = await servicio.ObtenerCalendario(usuario, month, year);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("ObtenerPacientesCitados/{fecha}")]
        public async Task<ActionResult> ObtenerPacientesCitados(string fecha)
        {
            ICita servicio = new Cita(this.conf);
            try
            {
                var response = await servicio.ObtenerPacientesCitados(fecha);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("EnviarMsj")]
        public async Task<ActionResult> EnviarMsj([FromBody] MensajeTextoModelo msj)
        {
            ICita servicio = new Cita(this.conf);
            try
            {
                var response = await servicio.EnviarMSJ(msj);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
