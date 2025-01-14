using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid³owy format adresu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Has³o jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
