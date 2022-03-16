using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Modelos.Entorno;
using Core.Servicios.Impl;
using Core.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendClinica.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ConsultaController : Controller
    {
        private Configuracion conf;
        public ConsultaController(Configuracion conf)
        {
            this.conf = conf;
        }
        //[Authorize]
        [HttpGet("ObtenerCatalogo/{catalogo}")]
        public async Task<ActionResult> ObtenerCatalogo(string catalogo)
        {
            IConsulta servicio = new Consulta(this.conf);
            try
            {

                var response = await servicio.ObtenerCatalogo(catalogo);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        //[Authorize]
        [HttpGet("ObtenerLocalidad")]
        public async Task<ActionResult> ObtenerLocalidad(string catalogo)
        {
            IConsulta servicio = new Consulta(this.conf);
            try
            {

                var response = await servicio.ObtenerLocalidad();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("Insertar")]
        public async Task<ActionResult> CrearConsulta([FromBody]ConsultaModelo consulta)
        {
            IConsulta servicio = new Consulta(this.conf);
            try
            {

                var response = await servicio.CrearConsulta(consulta);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("Editar")]
        public async Task<ActionResult> EditarConsulta([FromBody] ConsultaModelo consulta)
        {
            IConsulta servicio = new Consulta(this.conf);
            try
            {

                var response = await servicio.EditarConsulta(consulta);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("ObtenerConsulta/{idConsulta}/{tipoConsulta}")]
        public async Task<ActionResult> ObtenerConsulta(string idConsulta, string tipoConsulta)
        {
            IConsulta servicio = new Consulta(this.conf);
            try
            {

                var response = await servicio.ObtenerConsulta(idConsulta, tipoConsulta);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpPost("ObtenerConsultas")]
        public async Task<ActionResult> ObtenerConsultas([FromBody]CriterioConsultaModelo data)
        {
            IConsulta servicio = new Consulta(this.conf);
            try
            {

                var response = await servicio.ObtenerConsultas(data);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
