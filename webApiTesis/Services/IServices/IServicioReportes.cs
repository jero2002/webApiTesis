using webApiTesis.DTOs;

namespace webApiTesis.Services.IServices
{
    public interface IServicioReportes
    {
        Task<List<DTOJugadoresXProvincia>> GetJugadoresPorProvincia();
        Task<List<DTOJugadoresXPosicion>> GetJugadoresPorPosicion();
        Task<int> GetCantidadJugadores();
        Task<int> GetCantidadEquipos();
        Task<double> ObtenerEdadPromedioPorIdEquipo(int idEquipo);
        Task<List<DTOJugadoresXProvincia>> GetJugadoresPorProvincia(int idEquipo);
        Task<List<DTOEquipoXEstado>> ObtenerCantidadEquiposPorEstado(int idJugador);





    }
}
