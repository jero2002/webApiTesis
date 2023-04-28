using webApiTesis.Commands;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;

namespace webApiTesis.Services.IServices
{
    public interface IServicioEquipo
    {
        Task<ResultadoBase> PostEquipo(Equipo equipo);
        Task<List<EstadoEquipo>> GetEstadoEquipo();
        Task<List<GenerosEquipo>> GetGeneroEquipo();
        Task<List<Provincia>> GetProvincia();
        Task<ResultadoBase> PutEquipos(DTOequipos dtoequipos);
        Task<ComandoEquipo> GetEquipoByID(int id);
        Task<List<Equipo>> GetEquipoByIDGenero(int idGenero);

        Task<List<DTOEquiposJugador>> GetEquiposjugador(int idjugador); //dtoequiposjugador

    }
}
