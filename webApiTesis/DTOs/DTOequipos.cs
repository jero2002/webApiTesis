namespace webApiTesis.DTOs
{
    public class DTOequipos
    {
        public int IdEquipo { get; set; }

        public string Nombre { get; set; } = null!;

        public long Celular { get; set; }

        public int TorneoGanado { get; set; }

        public string Entrenador { get; set; } = null!;

        public int IdEstadoE { get; set; }

        public int IdGeneroE { get; set; }

        public int IdProvincia { get; set; }

    }
}
