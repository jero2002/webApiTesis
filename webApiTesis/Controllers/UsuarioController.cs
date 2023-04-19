using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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


    }
}
