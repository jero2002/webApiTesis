using webApiTesis.Commands;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;

namespace webApiTesis.Services.IServices
{
    public interface IServicioJugador
    {
        Task<ResultadoBase> PostJugador(Jugadore jugadore);
        Task<List<Posicione>> GetPosicion();
        Task<List<EstadoJugador>> GetEstadoJ();
        Task<List<Genero>> GetGenero();
        Task<ResultadoBase> PutJugadores(DTOJugadores dtoJugadores);
        Task<ComandoJugador> GetJugadorByID(int id);
        Task<List<Jugadore>> GetJugadorByIDGenero(int idGenero);
        Task<List<DTOJugadoresEquipo>> GetJugadoresequipo(int idjugador); //dtojugadoresequipo
        Task<ResultadoBase> DeleteEquipoJugador(int id);

        Task<ResultadoBase> postEquipoJugador(int idjugador, int idequipo);

    }
}
