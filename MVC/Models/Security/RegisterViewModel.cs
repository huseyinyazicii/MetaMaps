using System.ComponentModel.DataAnnotations;

namespace MVC.Models.Security
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(6, ErrorMessage = "Minimum 6 karakterli olmalıdır")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(6, ErrorMessage = "Minimum 6 karakterli olmalıdır")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(6, ErrorMessage = "Minimum 6 karakterli olmalıdır")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email'i düzgün girmelisiniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        public string Surname { get; set; }
    }
}
