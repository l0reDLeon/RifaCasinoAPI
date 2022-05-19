namespace RifaCasinoAPI.DTOs
{
    public class RespuestaAutenticacion
    {
        public string Token { get; set; }
        public DateTime Caducidad { get; set; }
    }
}
