using Microsoft.AspNetCore.Mvc;
using webApiTesis.Models;
using webApiTesis.Results;

namespace webApiTesis.Services.IServices
{
    public interface IServicioRegister
    {
        Task<ResultadoBase> PostRegister([FromBody] Usuario u);
    }
}
