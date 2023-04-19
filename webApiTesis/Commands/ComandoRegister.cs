using System.ComponentModel.DataAnnotations;

namespace webApiTesis.Commands
{
    public class ComandoRegister
    {
        [Required(ErrorMessage = "El Nombre es requerido.")]
        public string Nombre { get; set; }
     
        [Required(ErrorMessage = "El email es requerido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "La contraseña es requerida.")]
        [MinLength(5)]
        public string Contrasenia { get; set; }
    }
}
