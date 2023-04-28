using webApiTesis.Commands;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;

namespace webApiTesis.Services.IServices
{
    public interface IServicioUsuario
    {
        Task<ComandoLogin> GetUsuarioByID(int id);
        Task<ResultadoBase> PutUsuario(DTOUsuarioUpdate usuario);
    }
}
