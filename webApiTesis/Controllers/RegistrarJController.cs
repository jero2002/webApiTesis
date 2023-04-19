using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiTesis.Commands;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrarJController : ControllerBase
    {
        private readonly IServicioJugador servicio;
        private readonly ISecurityService _securityService;

        public RegistrarJController(IServicioJugador _servicio, DreamTeamContext context, ISecurityService securityService)
        {
            this.servicio = _servicio;
            this._securityService = securityService;
        }

        [HttpPost("PostJugador")]
        public async Task<ActionResult<ResultadoBase>> PostJugador([FromBody] ComandoRegistrarJugador comando)
        {
            int idUsuario = _securityService.GetUserId();

            Jugadore p = new Jugadore();
            p.IdJugador = idUsuario;
            p.Nombre = comando.Nombre;
            p.Celular = comando.Celular;
            p.Edad = comando.Edad;
            p.Descripcion = comando.Descripcion;
            p.IdPosicion = comando.IdPosicion;
            p.IdEstadoJ = comando.IdEstadoJ;
            p.IdProvincia = comando.IdProvincia;
            p.IdGenero = comando.IdGenero;


            return Ok(await this.servicio.PostJugador(p));
        }

     

        [HttpGet("GetPosicion")]
        public async Task<ActionResult> GetPosicion()
        {
            return Ok(await this.servicio.GetPosicion());
        }

        [HttpGet("GetEstadoJ")]
        public async Task<ActionResult> GetEstadoJ()
        {
            return Ok(await this.servicio.GetEstadoJ());
        }

        [HttpGet("GetGenero")]
        public async Task<ActionResult> GetGenero()
        {
            return Ok(await this.servicio.GetGenero());
        }

        [HttpPut("PutJugador")]
        public async Task<ActionResult<ResultadoBase>> PutJugadores([FromBody] DTOJugadores dtojugadores)
        {
            if (dtojugadores == null)
            {
                return BadRequest("El objeto dtojugadores está vacío");
            }

            return Ok(await this.servicio.PutJugadores(dtojugadores));
        }

        [HttpGet("getJugadorByID/{id}")]
        public async Task<IActionResult> GetJugadorByID(int id)
        {
            return Ok(await servicio.GetJugadorByID(id));
        }

        [HttpGet("GetJugadorByIDGenero/{idGenero}")]
        public async Task<IActionResult> GetJugadorByIDGenero(int idGenero)
        {
            return Ok(await this.servicio.GetJugadorByIDGenero(idGenero));
        }



    }
}
