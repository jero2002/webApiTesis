using webApiTesis.DTOs;
using webApiTesis.Results;

namespace webApiTesis.Services.IServices
{
    public interface IServiceNotificacion
    {
        Task<ResultadoBase> postNotificacionJugadorxEquipo(int idjugador, int idequipo); //va a notificaciones
        Task<ResultadoBase> postNotificacionEquipoxJugador(int idequipo, int idjugador); //va a notificacionesEJ

        Task<List<DTONotificacionParaEquipo>> NotificacionParaEquipo(int idequipo); //este trae el de notificaciones
        Task<List<DTONotificacionParaJugador>> NotificacionParaJugador(int idjugador); //este trae el de notificacionesEJ

        Task<ResultadoBase> Deletenotificacion(int id);
        Task<ResultadoBase> DeletenotificacionEJ(int id);


    }
}
