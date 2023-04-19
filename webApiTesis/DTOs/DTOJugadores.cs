namespace webApiTesis.DTOs
{
    public class DTOJugadores
    {
        public int IdJugador { get; set; }

        public string Nombre { get; set; } = null!;

        public long Celular { get; set; }

        public int Edad { get; set; }

        public string Descripcion { get; set; } = null!;

        public int IdPosicion { get; set; }

        public int IdEstadoJ { get; set; }

        public int IdProvincia { get; set; }

        public int IdGenero { get; set; }


    }
}
