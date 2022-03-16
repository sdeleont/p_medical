using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Modelos.Entorno;
using Core.Modelos.Tests;
using Core.Servicios.Impl;
using Core.Servicios.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackendClinica.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private IConfiguration _config;
        // Basicamente en el proyecto BackendClinica unicamente van configuraciones de entrypoints y clases controladoras
        // En el proyecto Core esta dividido en 3 partes
        // Carpeta Modelos almacena todas las clases modelo que se utilizan tanto para obtener datos de dapper como los modelos para la entrada
        // Carpeta Servicios contiene las interfaces y las clases de implementacion donde se realiza toda la logica necesaria del servicio
        // Carpeta Repositorios es la que se encarga de almacenar las clases que van a realizar directamente las operaciones a la base de datos
        //esto con el objetivo que si en otro servicio necesitamos realizar la misma operacion unicamente se utilize el metodo de Repositorios necesario
        private Configuracion conf;
        public UsuariosController(Configuracion conf, IConfiguration config) {
            this.conf = conf;
            _config = config;
        }
        // Para unicamente obtener informacion se utiliza el metodo GET
        // La url para llegar a este metodo seria api/Controlador/Usuarios/{usuario}
        // si los parametros vienen en la URL se pueden colocar dentro de {} y solo declarar la variable
        [Authorize]
        [HttpGet("Usuarios/{usuario}")]
        public async Task<ActionResult> ObtenerUsuario(string usuario)
        {
            // se realiza la instancia de la clase de servicio utilizando la interfaz de por medio y solo en esta parte se envia la configuracion
            // la estructura en todos los metodos y contradores es siempre la misma
            IUsuario servicio = new Usuario(this.conf);
            try
            {
                // se va a obtener los valores del metodo que deseamos del servicio
                // El Flujo de las operaciones es:
                //1) Proyecto BackendClinica ->Clase controladora
                //2) Proyecto Core -> Servicios-> Interfaces-> Interfaz del controlador
                //3) Proyecto Core -> Servicios-> Impl-> Clase que implementa metodos de la interfaz
                    //En esta clase se implementa toda la logica necesaria, exceptuando operaciones a la base de datos
                //4) Proyecto Core -> Repositorios-> Clase que implementa Operaciones a la base de datos
                var response = await servicio.ObtenerUsuario(usuario);
                // se responde con un OK en caso sea exitos
                return Ok(response);
            }
            catch (Exception ex)
            {
                // en caso sucede una excepcion se response con un status 500
                return StatusCode(500);
            }
        }

        [Authorize]
        //[AllowAnonymous]
        [HttpGet("Usuarios")]
        public async Task<ActionResult> ObtenerUsuarios()
        {
            IUsuario servicio = new Usuario(this.conf);
            try
            {
                var response = await servicio.ObtenerUsuarios();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        //[Authorize]
        [Authorize]
        [HttpPost("Insertar")]
        public async Task<ActionResult> CrearUsuario([FromBody]UsuarioModelo usuario)
        {
            IUsuario servicio = new Usuario(this.conf);
            try
            {

                var response = await servicio.CrearUsuario(usuario);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Authorize]
        [HttpGet("Eliminar/{idUsuario}")]
        public async Task<ActionResult> EliminarUsuario(string idUsuario)
        {
            IUsuario servicio = new Usuario(this.conf);
            try
            {

                var response = await servicio.EliminarUsuario(idUsuario);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [AllowAnonymous]
        [HttpPost("Auth")]
        public async Task<IActionResult> AutenticarUsuario([FromBody]UsuarioAuth usuario)
        {
            IUsuario servicio = new Usuario(this.conf);
            try
            {
                IActionResult response = Unauthorized();
                var responsePAuth = await servicio.AutenticarUsuario(usuario);

                if (responsePAuth.status.Equals("OK")) {
                    responsePAuth.token = GenerateJSONWebToken(responsePAuth.user);
                    response = Ok(responsePAuth);
                } 
                return response;

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        private string GenerateJSONWebToken(UsuarioModelo userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              null,
              expires: DateTime.Now.AddDays(7),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [Authorize]
        [HttpPost("CambiarPassword")]
        public async Task<IActionResult> CambiarPassword([FromBody] PasswordModelo password)
        {
            IUsuario servicio = new Usuario(this.conf);
            try
            {

                var response = await servicio.CambiarPassword(password);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }

        [Authorize]
        [HttpPost("ResetearPassword")]
        public async Task<IActionResult> ResetearPassword([FromBody] PasswordModelo password)
        {
            IUsuario servicio = new Usuario(this.conf);
            try
            {

                var response = await servicio.ResetearPassword(password);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
