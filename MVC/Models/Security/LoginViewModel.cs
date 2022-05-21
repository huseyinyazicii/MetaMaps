using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models.Security
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zorunlu alandır")]
        public string UserMail { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        public string Password { get; set; }
    }
}
