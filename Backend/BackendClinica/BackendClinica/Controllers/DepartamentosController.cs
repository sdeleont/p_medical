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
    public class DepartamentosController : Controller
    {
        private Configuracion conf;
        public DepartamentosController(Configuracion conf)
        {
            this.conf = conf;
        }
        [HttpGet("Departamentos/{id_pais}")]
        public async Task<ActionResult> ObtenerRoles(string id_pais)
        {
            IDepartamento servicio = new Departamento(this.conf);
            try
            {

                var response = await servicio.ObtenerDepartamentos(id_pais);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
