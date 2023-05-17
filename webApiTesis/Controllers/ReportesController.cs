using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Services.IServices;

namespace webApiTesis.Controllers
{
    
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

        [HttpGet("cantidadjugadores")]
        public async Task<ActionResult<int>> GetCantidadJugadores()
        {
                var cantidadJugadores = await servicio.GetCantidadJugadores();
                return Ok(cantidadJugadores);   
        }

        [HttpGet("cantidadequipos")]
        public async Task<ActionResult<int>> GetCantidadEquipos()
        {
            var cantidadequipos = await servicio.GetCantidadEquipos();
            return Ok(cantidadequipos);
        }

        [HttpGet("EdadPromedioPorIdEquipo/{idEquipo}")]
        public async Task<double> Get(int idEquipo)
        {
            return await servicio.ObtenerEdadPromedioPorIdEquipo(idEquipo);
        }

        [HttpGet("JugadoresXProvincia/{idEquipo}")]
        public async Task<ActionResult<List<DTOJugadoresXProvincia>>> GetJugadoresPorProvincia(int idEquipo)
        {
            var jugadoresPorProvincia = await servicio.GetJugadoresPorProvincia(idEquipo);

            if (jugadoresPorProvincia == null || !jugadoresPorProvincia.Any())
            {
                return BadRequest("No se encontraron datos");
            }

            return Ok(jugadoresPorProvincia);
        }


        [HttpGet("cantidadEquiposXEstado/{idJugador}")]
        public async Task<ActionResult<List<DTOEquipoXEstado>>> ObtenerCantidadEquiposPorEstado(int idJugador)
        {
            var equiposPorEstado = await servicio.ObtenerCantidadEquiposPorEstado(idJugador);

            if (equiposPorEstado == null || !equiposPorEstado.Any())
            {
                return BadRequest("No se encontraron datos");
            }

            return Ok(equiposPorEstado);
        }



    }
}
