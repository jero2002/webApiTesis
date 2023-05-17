using Microsoft.EntityFrameworkCore;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
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



        public async Task<List<DTOJugadoresXPosicion>> GetJugadoresPorPosicion()
        {
            var jugadoresPorPosicion = await (from jugador in context.Jugadores
                                              join estado in context.EstadoJugadors on jugador.IdEstadoJ equals estado.IdEstadoJ
                                              join posicion in context.Posiciones on jugador.IdPosicion equals posicion.IdPosicion
                                              where estado.IdEstadoJ == 1
                                              group jugador by posicion.Nombre into g
                                              select new DTOJugadoresXPosicion
                                              {
                                                  Posicion = g.Key,
                                                  CantidadJugadores = g.Count()
                                              }).ToListAsync();

            return jugadoresPorPosicion;
        }

        public async Task<int> GetCantidadJugadores()
        {
            var cantidadJugadores = await (from jugador in context.Jugadores
                                           select jugador).CountAsync();

            return cantidadJugadores;
        }

        public async Task<int> GetCantidadEquipos()
        {
            var cantidadJugadores = await (from Equipo in context.Equipos
                                           select Equipo).CountAsync();

            return cantidadJugadores;
        }

        public async Task<double> ObtenerEdadPromedioPorIdEquipo(int idEquipo)
        {
            var jugadoresEquipo = await context.EquiposJugadores
                .Include(je => je.IdJugadorNavigation)
                .Where(je => je.IdEquipo == idEquipo)
                .ToListAsync();

            if (jugadoresEquipo.Any())
            {
                double edadPromedio = jugadoresEquipo.Average(je => je.IdJugadorNavigation.Edad);
                return edadPromedio;
            }
            else
            {
                return 0;
            }
        }

        public async Task<List<DTOJugadoresXProvincia>> GetJugadoresPorProvincia(int idEquipo)
        {
            var jugadoresPorProvincia = await (from jugador in context.Jugadores
                                               join provincia in context.Provincias on jugador.IdProvincia equals provincia.IdProvincia
                                               join equipoJugador in context.EquiposJugadores on jugador.IdJugador equals equipoJugador.IdJugador
                                               where equipoJugador.IdEquipo == idEquipo
                                               group jugador by provincia.Nombre into g
                                               select new DTOJugadoresXProvincia
                                               {
                                                   Provincia = g.Key,
                                                   CantidadJugadores = g.Count()
                                               }).ToListAsync();

            return jugadoresPorProvincia;
        }

        public async Task<List<DTOEquipoXEstado>> ObtenerCantidadEquiposPorEstado(int idJugador)
        {
            var equiposPorEstado = await context.Equipos
                .Where(equipo => equipo.EquiposJugadores.Any(equipoJugador => equipoJugador.IdJugador == idJugador))
                .GroupBy(equipo => new { equipo.IdEstadoE, equipo.IdEstadoENavigation.Nombre })
                .Select(grupo => new DTOEquipoXEstado
                {
                    NombreEstadoEquipo = grupo.Key.Nombre,
                    CantidadEquipos = grupo.Count()
                })
                .ToListAsync();
            return equiposPorEstado;
        }



    }
}
