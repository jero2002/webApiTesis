using System.ComponentModel.DataAnnotations;

namespace webApiTesis.Commands
{
    public class ComandoEquipo
    {
        public int IdEquipo { get; set; }

        [Required(ErrorMessage = "El nombre del equipo es requerido.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El número de celular del equipo es requerido.")]
        public long Celular { get; set; }

        [Required(ErrorMessage = "El número de torneos ganados del equipo es requerido.")]
        public int TorneoGanado { get; set; }

        [Required(ErrorMessage = "El nombre del entrenador del equipo es requerido.")]
        public string Entrenador { get; set; }

        [Required(ErrorMessage = "El id del estado del equipo es requerido.")]
        public int IdEstadoE { get; set; }

        [Required(ErrorMessage = "El id del género del equipo es requerido.")]
        public int IdGeneroE { get; set; }

        [Required(ErrorMessage = "El id de la provincia del equipo es requerido.")]
        public int IdProvincia { get; set; }
    }
}
