using System.Text.RegularExpressions;
using webApiTesis.Comun;
using webApiTesis.Data;
using webApiTesis.Models;
using webApiTesis.Results;
using webApiTesis.Services.IServices;

namespace webApiTesis.Services
{
    public class ServicioRegister : IServicioRegister
    {
        private readonly DreamTeamContext context;
        private readonly ISecurityService _securityService;

        public ServicioRegister(DreamTeamContext _context, ISecurityService securityService) { this.context = _context; _securityService = securityService; }

        public async Task<ResultadoBase> PostRegister(Usuario u)
        {
            ResultadoBase resultado = new ResultadoBase();

            if (this.ValidarMail(u.Email))
            {
                if (this.validarExpresion(u.Email))
                {

                        try
                        {
                            await context.AddAsync(u);
                            await context.SaveChangesAsync(_securityService.GetUserName() ?? Constantes.DefaultSecurityValues.DefaultUserName);
                            resultado.Ok = true;
                            resultado.CodigoEstado = 200;
                            return resultado;

                        }
                        catch (Exception)
                        {
                            resultado.Ok = false;
                            resultado.CodigoEstado = 400;
                            resultado.Error = "Error al registrar un usuario";
                            return resultado;
                        }
                
                }
                resultado.Ok = false;
                resultado.CodigoEstado = 400;
                resultado.Error = "El correo no es valido, utilice expresiones correspondientes";
                return resultado;

            }
            resultado.Ok = false;
            resultado.CodigoEstado = 400;
            resultado.Error = "Ya existe el correo ingresado";
            return resultado;

        }

        private bool ValidarMail(string email)
            {
                var usuario = context.Usuarios.Where(c => c.Email.Equals(email)).FirstOrDefault();
                if (usuario != null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
           

            private bool validarExpresion(string email)
            {
                return email != null && Regex.IsMatch(email, "^(([\\w-]+\\.)+[\\w-]+|([a-zA-Z]{1}|[\\w-]{2,}))@(([a-zA-Z]+[\\w-]+\\.){1,2}[a-zA-Z]{2,4})$");
            }

       
    }

}

