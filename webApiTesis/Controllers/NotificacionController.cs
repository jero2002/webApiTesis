using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiTesis.Data;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {

        private readonly IServiceNotificacion servicio;
        private readonly ISecurityService _securityService;

        public NotificacionController(IServiceNotificacion _servicio, DreamTeamContext _contextm, ISecurityService securityService)
        {
            this.servicio = _servicio;
            _securityService = securityService;
        }

        [HttpPost("je/{idjugador}/{idequipo}")]
        public async Task<ActionResult<ResultadoBase>> postNotificacionJugadorxEquipo(int idjugador, int idequipo)
        {
            var resultado = await servicio.postNotificacionJugadorxEquipo(idjugador, idequipo);
            return resultado;
        }

        [HttpPost("ej/{idequipo}/{idjugador}")]
        public async Task<ActionResult<ResultadoBase>> postNotificacionEquipoxJugador(int idequipo, int idjugador)
        {
            var resultado = await servicio.postNotificacionEquipoxJugador(idequipo, idjugador);
            return resultado;
        }

        [HttpGet("getnotificacionequipo/{idequipo}")]
        public async Task<IActionResult> NotificacionParaEquipo(int idequipo)
        {
            return Ok(await servicio.NotificacionParaEquipo(idequipo));
        }


        [HttpGet("getnotificacionjugador/{idjugador}")]
        public async Task<IActionResult> NotificacionParaJugador(int idjugador)
        {
            return Ok(await servicio.NotificacionParaJugador(idjugador));
        }

        [HttpDelete]
        [Route("DeleteNotificacion/{id}")]
        public async Task<ActionResult<ResultadoBase>> Deletenotificacion(int id)
        {
            return Ok(await this.servicio.Deletenotificacion(id));
        }

        [HttpDelete]
        [Route("DeleteNotificacionej/{id}")]
        public async Task<ActionResult<ResultadoBase>> DeletenotificacionEJ(int id)
        {
            return Ok(await this.servicio.DeletenotificacionEJ(id));
        }



    }
}

