namespace webApiTesis.DTOs
{
    public class DTOEquiposJugador
    {
        public int idEquipoJugador { get; set; }
        public string Nombre { get; set; } = null!;

        public long Celular { get; set; }

        public int TorneoGanado { get; set; }

        public string Entrenador { get; set; } = null!;
    }
}
