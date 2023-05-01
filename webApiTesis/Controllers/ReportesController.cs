using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IServicioReportes servicio;
        private readonly ISecurityService _securityService;

        public ReportesController(IServicioReportes _servicio, DreamTeamContext _contextm, ISecurityService securityService)
        {
            this.servicio = _servicio;
            _securityService = securityService;
        }


        [HttpGet("jugadoresporprovincia")]
        public async Task<ActionResult<List<DTOJugadoresXProvincia>>> GetJugadoresPorProvincia()
        {
            var jugadoresPorProvincia = await servicio.GetJugadoresPorProvincia();
            return Ok(jugadoresPorProvincia);
        }

        [HttpGet("jugadoresporposicion")]
        public async Task<ActionResult<List<DTOJugadoresXPosicion>>> GetJugadoresPorPosicion()
        {
            var jugadoresPorPosicion = await servicio.GetJugadoresPorPosicion();
            return Ok(jugadoresPorPosicion);
        }

    }
}
