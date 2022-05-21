using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models
{
    public class AddRoadMapModel
    {
        [Required(ErrorMessage ="İsim alanı zorunludur")]
        [MaxLength(100, ErrorMessage = "Maximum 100 karakter içerebilir")]
        [MinLength(10, ErrorMessage = "Minimum 10 karakter içerebilir")]
        public string Name { get; set; }

        public IFormFile Image { get; set; }

        [Required(ErrorMessage = "Açıklama alanı zorunludur")]
        [MaxLength(500, ErrorMessage = "Maximum 500 karakter içerebilir")]
        [MinLength(20, ErrorMessage = "Minimum 20 karakter içerebilir")]
        public string Description { get; set; }
    }
}
