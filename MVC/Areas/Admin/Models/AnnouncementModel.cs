using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models
{
    public class AnnouncementModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık alanı zorunludur.")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakter içermelidir.")]
        [MaxLength(100,ErrorMessage = "Maximum 100 karakter girilebilir.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "İçerik alanı zorunludur.")]
        [MinLength(10, ErrorMessage = "Minimum 10 karakter içermelidir.")]
        [MaxLength(500, ErrorMessage = "Maximum 500 karakter girilebilir.")]
        public string Content { get; set; }
    }
}
