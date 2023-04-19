using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using webApiTesis.Commands;
using webApiTesis.Data;
using webApiTesis.Models;
using webApiTesis.Services.IServices;
using System.Security.Cryptography;

namespace webApiTesis.Services
{
    public class ServicioLogin : IServicioLogin
    {
        private readonly IConfiguration config;
        private readonly DreamTeamContext context;

        public ServicioLogin(DreamTeamContext _context, IConfiguration _config)
        {
            this.context = _context;
            this.config = _config;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await context.Usuarios.AsNoTracking().ToListAsync();
        }


        public async Task<ComandoLogin> Login([FromBody] ComandoLogin comando)
        {
            ComandoLogin emailPass = new ComandoLogin();

            try
            {
                byte[] ePass = GetHash(comando.Contrasenia);

                emailPass = await context.Usuarios
                  .FirstOrDefaultAsync(c => c.Email == comando.Email && c.HashContrasena == ePass) ?? new ComandoLogin();

                if (emailPass != null)
                {
                    emailPass.Ok = true;
                    emailPass.CodigoEstado = 200;
                    emailPass.Error = "Cuenta Valida";
                    return emailPass;
                }
                else
                {
                    emailPass.Ok = false;
                    emailPass.CodigoEstado = 400;
                    emailPass.Error = ("Se necesita un email y contraseña");
                    return emailPass;
                }
            }
            catch (Exception ex)
            {
                emailPass.Ok = false;
                emailPass.CodigoEstado = 400;
                emailPass.Error = ex.Message;
                return emailPass;
            }
        }

        private byte[] GetHash(string key)
        {
            var bytes = Encoding.UTF8.GetBytes(key);
            return new SHA256Managed().ComputeHash(bytes);
        }

    }
}
