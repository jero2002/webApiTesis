using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiTesis.Commands;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarEController : ControllerBase
    {
        private readonly IServicioEquipo servicio;
        private readonly ISecurityService _securityService;

        public RegistrarEController(IServicioEquipo _servicio, DreamTeamContext _contextm, ISecurityService securityService)
        {
            this.servicio = _servicio;
            _securityService = securityService;
        }

        [HttpPost("PostEquipo")]
        public async Task<ActionResult<ResultadoBase>> PostEquipo([FromBody] ComandoRegistrarEquipo comando)
        {
            int idUsuario = _securityService.GetUserId();

            Equipo p = new Equipo();
            p.IdEquipo = idUsuario;
            p.Nombre = comando.Nombre;
            p.Celular = comando.Celular;
            p.TorneoGanado = comando.TorneoGanado;
            p.Entrenador = comando.Entrenador;
            p.IdEstadoE=comando.IdEstadoE;
            p.IdGeneroE=comando.IdGeneroE;
            p.IdProvincia=comando.IdProvincia;


            return Ok(await this.servicio.PostEquipo(p));
        }

        [HttpGet("GetEstadoEquipo")]
        public async Task<ActionResult> GetEstadoEquipo()
        {
            return Ok(await this.servicio.GetEstadoEquipo());
        }

        [HttpGet("GetGeneroEquipo")]
        public async Task<ActionResult> GetGeneroEquipo()
        {
            return Ok(await this.servicio.GetGeneroEquipo());
        }

        [HttpGet("GetProvincia")]
        public async Task<ActionResult> GetProvincia()
        {
            return Ok(await this.servicio.GetProvincia());
        }

        [HttpPut("PutEquipo")]
        public async Task<ActionResult<ResultadoBase>> PutEquipos([FromBody] DTOequipos dtoequipos)
        {
            if (dtoequipos == null)
            {
                return BadRequest("El objeto jugador está vacío");
            }

            return Ok(await this.servicio.PutEquipos(dtoequipos));
        }

        [HttpGet("getEquipoByID/{id}")]
        public async Task<IActionResult> GetEquipoByID(int id)
        {
            return Ok(await servicio.GetEquipoByID(id));
        }


        [HttpGet("GetEquipoByIDGenero/{idGenero}")]
        public async Task<IActionResult> GetEquipoByIDGenero(int idGenero)
        {
            return Ok(await this.servicio.GetEquipoByIDGenero(idGenero));
        }

        [HttpGet("GetEquiposjugador/{idjugador}")]
        public async Task<IActionResult> GetEquiposjugador(int idjugador)
        {
            return Ok(await this.servicio.GetEquiposjugador(idjugador));
        }


    }
}
