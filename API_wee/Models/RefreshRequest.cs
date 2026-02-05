namespace API_wee.Models
{
    public class RefreshRequest
    {
        public string RefreshToken { get; set; } = null!;
        public string AccessToken { get; set; } = null!; // opcional, usado para leer claims del token expirado
    }
}
