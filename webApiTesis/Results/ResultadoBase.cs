namespace webApiTesis.Results
{
    public class ResultadoBase
    {
        public string Message { set; get; } = null!;
        public bool Ok { set; get; }
        public string Error { get; set; }
        public int CodigoEstado { set; get; }
        public dynamic Resultado { get; set; }
        public dynamic respondeme { get; set; }
    }
}
