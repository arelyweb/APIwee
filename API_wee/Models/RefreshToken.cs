namespace API_wee.Models
{
    public class RefreshToken
    {
        public string Token { get; set; } = null!; // en producción guarda hash en DB
        public string Id_user { get; set; } = null!;
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
        public bool Revoked { get; set; }
        public string? ReplacedBy { get; set; }
    }
}
