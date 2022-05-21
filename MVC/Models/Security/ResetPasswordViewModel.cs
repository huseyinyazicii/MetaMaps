using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Security
{
    public class ResetPasswordViewModel
    {
        public string Code { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email'i düzgün girmelisiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(6, ErrorMessage = "Minimum 6 karakterli olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(6, ErrorMessage = "Minimum 6 karakterli olmalıdır")]
        public string ConfirmPassword { get; set; }
    }
}
