using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IServicioUsuario _servicioUsuarios;

        private readonly ISecurityService _securityService;

        public UsuarioController(IServicioUsuario servicioClientes, ISecurityService securityService)
        {
            _servicioUsuarios = servicioClientes;
            _securityService = securityService;
        }


        [HttpGet("getUsuarioByID/{id}")]
        public async Task<IActionResult> GetUsuarioByID(int id)
        {
            return Ok(await _servicioUsuarios.GetUsuarioByID(id));
        }

        [HttpPut("PutUsuario")]
        public async Task<ActionResult<ResultadoBase>> PutUsuario([FromBody] DTOUsuarioUpdate usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El objeto usuario está vacío");
            }

            return Ok(await this._servicioUsuarios.PutUsuario(usuario));
        }


    }
}
