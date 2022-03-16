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
    public class PaisesController : Controller
    {
        private Configuracion conf;
        public PaisesController(Configuracion conf)
        {
            this.conf = conf;
        }
        [AllowAnonymous]
        [HttpGet("Paises/")]
        public async Task<ActionResult> ObtenerRoles(string id_pais)
        {
            IPais servicio = new Pais(this.conf);
            try
            {

                var response = await servicio.ObtenerPais();
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
