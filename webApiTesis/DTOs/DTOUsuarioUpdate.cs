namespace webApiTesis.DTOs
{
    public class DTOUsuarioUpdate
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
