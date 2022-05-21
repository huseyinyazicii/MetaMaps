using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models
{
    public class StepModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim zorunlu alandır.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 karakter içerebilir")]
        [MinLength(5, ErrorMessage = "Minimum 5 karakter içermelidir.")]
        public string Name { get; set; }
    }
}
