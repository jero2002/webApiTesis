using Microsoft.EntityFrameworkCore;
using webApiTesis.Commands;
using webApiTesis.Data;
using webApiTesis.DTOs;
using webApiTesis.Models;
using webApiTesis.Results;
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

        public async Task<ResultadoBase> PutUsuario(DTOUsuarioUpdate usuario)
        {
            ResultadoBase resultado = new ResultadoBase();

            try
            {
                if (usuario == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El objeto usuario está vacío";
                    return resultado;
                }

                var Usuarioo = await _context.Usuarios.FindAsync(usuario.IdUsuario);

                if (Usuarioo == null)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 404;
                    resultado.Message = $"No se encontró un usuario con el id {usuario.IdUsuario}";
                    return resultado;
                }

                var emailExistente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == usuario.Email);

                if (emailExistente != null && emailExistente.IdUsuario != usuario.IdUsuario)
                {
                    resultado.Ok = false;
                    resultado.CodigoEstado = 400;
                    resultado.Message = "El email ya existe";
                    return resultado;
                }

                Usuarioo.Nombre = usuario.Nombre;
                Usuarioo.Email = usuario.Email;

                _context.Update(Usuarioo);
                await _context.SaveChangesAsync();

                resultado.Ok = true;
                resultado.CodigoEstado = 200;
                resultado.Message = "El usuario fue modificado correctamente";
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoEstado = 500;
                resultado.Message = "Hubo un error al modificar el usuario";
                resultado.Error = ex.ToString();
            }

            return resultado;
        }






    }
}
