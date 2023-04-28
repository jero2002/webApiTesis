using Microsoft.EntityFrameworkCore;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioReportes : IServicioReportes
    {
        private readonly DreamTeamContext context;
        private readonly ISecurityService _securityService;

        public ServicioReportes(DreamTeamContext _context, ISecurityService securityService)
        {
            this.context = _context;
            _securityService = securityService;
        }
        public async Task<List<DTOJugadoresXProvincia>> GetJugadoresPorProvincia()
        {
            var jugadoresPorProvincia = await (from jugador in context.Jugadores
                                               join provincia in context.Provincias on jugador.IdProvincia equals provincia.IdProvincia
                                               group jugador by provincia.Nombre into g
                                               select new DTOJugadoresXProvincia
                                               {
                                                   Provincia = g.Key,
                                                   CantidadJugadores = g.Count()
                                               }).ToListAsync();
            return jugadoresPorProvincia; 
        }

    }
}
