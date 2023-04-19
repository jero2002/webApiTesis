using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using webApiTesis.Commands;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;
using System.Security.Cryptography;

namespace webApiTesis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IServicioRegister servicio;

        public RegisterController(IServicioRegister _servicio)
        {
            this.servicio = _servicio;
        }

        [HttpPost("PostRegister")]
        public async Task<ActionResult<ResultadoBase>> PostRegister([FromBody] ComandoRegister comando)
        {
            Usuario r = new Usuario();

            byte[] ePass = GetHash(comando.Contrasenia);

            r.Nombre = comando.Nombre;
            r.Email = comando.Email;
            r.HashContrasena = ePass;
            r.IdRol = 2;
            r.IdEquipo = null;
            r.IdJugador=null;
            
            return Ok(await this.servicio.PostRegister(r));
        }

        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }
    }
}
