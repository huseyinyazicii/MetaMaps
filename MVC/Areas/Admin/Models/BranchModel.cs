using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models
{
    public class BranchModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "İsim zorunlu alandır.")]
        [MaxLength(100, ErrorMessage = "Maximum 100 karakter içerebilir")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakter içermelidir.")]
        public string Name { get; set; }
    }
}
