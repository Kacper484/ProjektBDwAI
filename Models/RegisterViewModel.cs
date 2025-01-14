using System.ComponentModel.DataAnnotations;

namespace Aplikacja_na_BDwAI.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email jest wymagany.")]
        [EmailAddress(ErrorMessage = "Nieprawid�owy format adresu email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Has�o jest wymagane.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Potwierdzenie has�a jest wymagane.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Has�a musz� by� takie same.")]
        public string ConfirmPassword { get; set; }
    }
}
