namespace webApiTesis.DTOs
{
    public class DTOJugadoresEquipo
    {
        public int idEquipoJugador {get; set;}
        public string Nombre { get; set; } = null!;

        public long Celular { get; set; }

        public int Edad { get; set; }

        public string Posicion { get; set; }

    }
}
