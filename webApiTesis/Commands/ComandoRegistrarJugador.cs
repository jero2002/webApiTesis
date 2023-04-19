using System.ComponentModel.DataAnnotations;

namespace webApiTesis.Commands
{
    public class ComandoRegistrarJugador
    {
        [Required(ErrorMessage = "El nombre del jugador es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El número de celular del jugador es requerido.")]
        public long Celular { get; set; }

        [Required(ErrorMessage = "La edad del jugador es requerida.")]
        public int Edad { get; set; }

        [Required(ErrorMessage = "La nombre descripcion del jugador es requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El id de la posicion del jugador es requerido.")]
        public int IdPosicion{ get; set; }

        [Required(ErrorMessage = "El id del estado del jugador es requerido.")]
        public int IdEstadoJ { get; set; }

        [Required(ErrorMessage = "El id de la provincia del jugador es requerido.")]
        public int IdProvincia { get; set; }

        [Required(ErrorMessage = "El id del género del jugador es requerido.")]
        public int IdGenero { get; set; }


    }
}
