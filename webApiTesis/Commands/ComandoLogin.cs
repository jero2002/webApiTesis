using System.ComponentModel.DataAnnotations;
using webApiTesis.Models;

namespace webApiTesis.Commands
{
    public class ComandoLogin
    {
        public int IdUsuario { get; set; }

        #region Command
        [Required(ErrorMessage = "El email es requerido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contrseña es requerida")]
        public string Contrasenia { get; set; }
        #endregion

        #region Response
        public string Nombre { get; set; }
        public int Rol{ get; set; }
        public string Token { get; set; }
        public int? IdJugador { get; set; }
        public int? IdEquipo { get; set; }
        #endregion

        #region Resultado Base
        public string Message { set; get; } = null!;
        public bool Ok { set; get; }
        public string Error { get; set; }
        public int CodigoEstado { set; get; }
        #endregion


        public static implicit operator ComandoLogin(Usuario user)
        {
            if (user == null)
                return null;

            return new ComandoLogin
            {
                IdUsuario = user.IdUsuario,
                Email = user.Email,
                Nombre = user.Nombre,          
                Rol = user.IdRol,
                IdJugador=user.IdJugador,
                IdEquipo = user.IdEquipo
            };
        }



    }
}
