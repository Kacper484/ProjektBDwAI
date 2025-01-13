namespace Aplikacja_na_BDwAI.Models
{
    public class User
    {
        public int Id { get; set; } // Klucz główny
        public string Email { get; set; }
        public string Password { get; set; }    
        public string Role { get; set; } //Rola użytkownika ("Admin", "User")
    }
}
