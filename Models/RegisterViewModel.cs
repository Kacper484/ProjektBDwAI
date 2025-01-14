using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid³owy format adresu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Has³o jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potwierdzenie has³a jest wymagane.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Has³a musz¹ byæ takie same.")]
        public string ConfirmPassword { get; set; }
    }
}
