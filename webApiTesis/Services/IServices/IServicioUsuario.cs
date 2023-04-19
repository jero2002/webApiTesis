using webApiTesis.Commands;

namespace webApiTesis.Services.IServices
{
    public interface IServicioUsuario
    {
        Task<ComandoLogin> GetUsuarioByID(int id);
    }
}
