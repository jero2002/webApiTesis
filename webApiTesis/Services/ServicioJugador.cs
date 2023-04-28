using Azure.Core.GeoJson;
using Microsoft.EntityFrameworkCore;
using webApiTesis.Commands;
using webApiTesis.Comun;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioJugador : IServicioJugador
    {

        private readonly DreamTeamContext context;
        private readonly ISecurityService _securityService;

        public ServicioJugador(DreamTeamContext _context, ISecurityService securityService)
        {
            this.context = _context;
            _securityService = securityService;
        }

        public async Task<ResultadoBase> PostJugador(Jugadore e)
        {
            ResultadoBase resultado = new ResultadoBase();
            try
            {
                await context.AddAsync(e);
                await context.SaveChangesAsync(_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName);

                Jugadore jugadorGuardado = await context.Jugadores.FindAsync(e.IdJugador);

                int userId = _securityService.GetUserId();
                Usuario usuario = await context.Usuarios.FindAsync(userId);

                usuario.IdJugador = jugadorGuardado.IdJugador;
                await context.SaveChangesAsync(_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName);

                return new ResultadoBase
                {
                    Ok = true,
                    CodigoEstado = 200,
                    Message = "El Jugador se guardó exitosamente."
                };
            }
            catch (Exception)
            {
                return new ResultadoBase
                {
                    Ok = false,
                    CodigoEstado = 400,
                    Message = "Error al ingresar un Jugador"
                };
            }

        }
        
        public async Task<List<Posicione>> GetPosicion()
        {
            return await this.context.Posiciones.AsNoTracking().ToListAsync();
        }

        public async Task<List<EstadoJugador>> GetEstadoJ()
        {
            return await this.context.EstadoJugadors.AsNoTracking().ToListAsync();
        }

        public async Task<List<Genero>> GetGenero()
        {
            return await this.context.Generos.AsNoTracking().ToListAsync();
        }


        public async Task<ResultadoBase> PutJugadores(DTOJugadores dtoJugadores)
        {
            ResultadoBase resultado = new ResultadoBase();

            try
            {
                if (dtoJugadores == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El objeto jugadores está vacío";
                    return resultado;
                }

                var jugadore = await context.Jugadores.FindAsync(dtoJugadores.IdJugador);

                if (jugadore == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un jugador con el id {dtoJugadores.IdJugador}";
                    return resultado;
                }

                jugadore.Nombre = dtoJugadores.Nombre;
                jugadore.Celular = dtoJugadores.Celular;
                jugadore.Edad = dtoJugadores.Edad;
                jugadore.Descripcion = dtoJugadores.Descripcion;
                jugadore.IdPosicion = dtoJugadores.IdPosicion;
                jugadore.IdEstadoJ = dtoJugadores.IdEstadoJ;
                jugadore.IdProvincia = dtoJugadores.IdProvincia;
                jugadore.IdGenero = dtoJugadores.IdGenero;

                context.Update(jugadore);
                await context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El jugador fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el jugador";
                resultado.Error = ex.ToString();
            }

            return resultado;
        }



        public async Task<ComandoJugador> GetJugadorByID(int id)
        {
            var jugador = await context.Jugadores
                .FirstOrDefaultAsync(x => x.IdJugador == id);
            ComandoJugador comando = new ComandoJugador();

            if (jugador != null)
            {
                comando.IdJugador = jugador.IdJugador;
                comando.Nombre = jugador.Nombre;
                comando.Celular = jugador.Celular;         
                comando.Edad = jugador.Edad;
                comando.Descripcion = jugador.Descripcion;
                comando.IdPosicion = jugador.IdPosicion;
                comando.IdEstadoJ = jugador.IdEstadoJ;
                comando.IdProvincia = jugador.IdProvincia;
                comando.IdGenero = jugador.IdGenero;
            }
            return comando;
        }



        public async Task<List<Jugadore>> GetJugadorByIDGenero(int idGenero)
        {
            IQueryable<Jugadore> query = context.Jugadores
                .Include(x => x.IdEstadoJNavigation)
                .Include(x => x.IdGeneroNavigation)
                .Include(x => x.IdPosicionNavigation)
                .Include(x => x.IdProvinciaNavigation)
                .AsNoTracking();

            if (idGenero == 3)
            {
                query = query.Where(x => x.IdGeneroNavigation.IdGenero <= idGenero);
            }
            else if (idGenero == 1)
            {
                query = query.Where(x => x.IdGeneroNavigation.IdGenero == 1 || x.IdGeneroNavigation.IdGenero == 3);
            }
            else if (idGenero == 2)
            {
                query = query.Where(x => x.IdGeneroNavigation.IdGenero == 2 || x.IdGeneroNavigation.IdGenero == 3);
            }

            return await query.ToListAsync();
        }

        public async Task<List<DTOJugadoresEquipo>> GetJugadoresequipo(int idequipo)
        {
            ResultadoBase resultado = new ResultadoBase();

            var jugadores = await context.EquiposJugadores
                .Where(x => x.IdEquipo == idequipo) // Filtras por el idJugador
                .Select(x => new DTOJugadoresEquipo
                {
                    idEquipoJugador = x.IdEquipoJugador,
                    Nombre = x.IdJugadorNavigation.Nombre,
                    Edad = x.IdJugadorNavigation.Edad,
                    Celular = x.IdJugadorNavigation.Celular,
                    Posicion = x.IdJugadorNavigation.IdPosicionNavigation.Nombre
                })
                .AsNoTracking()
                .ToListAsync();

            return jugadores;
        }

        public async Task<ResultadoBase> DeleteEquipoJugador(int id)
        {
            ResultadoBase resultado = new ResultadoBase();
            var equiposJugadore = await context.EquiposJugadores.Where(c => c.IdEquipoJugador.Equals(id)).FirstOrDefaultAsync();
            if (equiposJugadore != null)
            {
                context.EquiposJugadores.Remove(equiposJugadore); // Elimina el registro
                await context.SaveChangesAsync(); // Guarda los cambios en la base de datos
                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "Eliminaste exitosamente";
            }
            else
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Message = "Error al eliminar";
                return resultado;
            }

            return resultado;
        }


    }
}
