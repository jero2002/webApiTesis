using Microsoft.EntityFrameworkCore;
using webApiTesis.Commands;
using webApiTesis.Data;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioUsuario : IServicioUsuario
    {

        private readonly DreamTeamContext _context;
        private readonly ISecurityService _securityService;

        public ServicioUsuario(DreamTeamContext context, ISecurityService securityService)
        {
            _context = context;
            _securityService = securityService;
        }


        public async Task<ComandoLogin> GetUsuarioByID(int id)
        {
            var usuario = await _context.Usuarios
                
                .FirstOrDefaultAsync(x => x.IdUsuario == id);
            ComandoLogin comando = new ComandoLogin();

            if (usuario != null)
            {
                comando.IdUsuario = usuario.IdUsuario;
                comando.Email = usuario.Email;
                comando.Nombre = usuario.Nombre;          //si algo falla es por lo del token aca o algo asi
                comando.Rol = usuario.IdRol;
                comando.IdEquipo = usuario.IdEquipo;
                comando.IdJugador = usuario.IdJugador;

            }
            return comando;
        }




    }
}
