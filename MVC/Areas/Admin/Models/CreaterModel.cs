using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models
{
    public class CreaterModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim alanı boş bırakılamaz")]
        [MaxLength(50, ErrorMessage = "Maximum 50 karakter içerebilir")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakter içermelidir")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim alanı boş bırakılamaz")]
        [MaxLength(50, ErrorMessage = "Maximum 50 karakter içerebilir")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakter içermelidir")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz")]
        [MaxLength(100, ErrorMessage = "Maximum 100 karakter içerebilir")]
        [EmailAddress]
        public string Email { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Detay alanı boş bırakılamaz")]
        [MaxLength(200, ErrorMessage = "Maximum 200 karakter içerebilir")]
        [MinLength(10, ErrorMessage = "Minimum 10 karakter içermelidir")]
        public string Details { get; set; }

        public string Linkedin { get; set; }
        public string Github { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
    }
}
