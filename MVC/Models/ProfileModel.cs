using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class ProfileModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        public string Surname { get; set; }

        public IFormFile ImagePath { get; set; }

        public string Image { get; set; }
    }
}
