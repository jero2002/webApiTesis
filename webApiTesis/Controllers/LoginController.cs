using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webApiTesis.Commands;
using webApiTesis.Data;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IServicioLogin servicio;


        public LoginController(IServicioLogin _servicio,
           IConfiguration config,
           DreamTeamContext context)
        {
            this.servicio = _servicio;
            _config = config;
        }

        [HttpGet("GetUsuarios")]
        public async Task<ActionResult> GetUsuarios()
        {
            var get = await this.servicio.GetUsuarios();
            return Ok(get);
        }

        [HttpPost("PostLogin")]

        public async Task<IActionResult> Login([FromBody] ComandoLogin comando)
        {
            var resultado = await this.servicio.Login(comando);

            if (resultado.Ok)
            {
                var claims = new[]
                {
                     new Claim(ClaimTypes.NameIdentifier, resultado.IdUsuario.ToString()),
                     new Claim(ClaimTypes.Name, resultado.Email),
                     new Claim(ClaimTypes.Role,  resultado.Rol.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(double.Parse(_config.GetSection("AppSettings:Expires").Value)),
                    SigningCredentials = creds,
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                resultado.Token = tokenHandler.WriteToken(token);

                return Ok(resultado);
            }

            return Unauthorized(resultado);
        }




    }
}
