namespace MasoksTech.Core
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "Cliente";
        public string NombreCompleto { get; set; } = string.Empty;
    }
}