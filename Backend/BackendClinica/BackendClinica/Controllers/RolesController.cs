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
    public class RolesController : Controller
    {
        private Configuracion conf;
        public RolesController(Configuracion conf)
        {
            this.conf = conf;
        }

        [AllowAnonymous]
        [HttpGet("Roles/")]
        public async Task<ActionResult> ObtenerRoles()
        {
            IRol servicio = new Rol(this.conf);
            try
            {

                var response = await servicio.ObtenerRoles();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
