using webApiTesis.DTOs;

namespace webApiTesis.Services.IServices
{
    public interface IServicioReportes
    {
        Task<List<DTOJugadoresXProvincia>> GetJugadoresPorProvincia();
    }
}
