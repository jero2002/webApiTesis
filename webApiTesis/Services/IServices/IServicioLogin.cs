using Microsoft.AspNetCore.Mvc;
using webApiTesis.Commands;
using webApiTesis.Models;

namespace webApiTesis.Services.IServices
{
    public interface IServicioLogin
    {
        Task<List<Usuario>> GetUsuarios();
        Task<ComandoLogin> Login([FromBody] ComandoLogin comando);
    }
}
