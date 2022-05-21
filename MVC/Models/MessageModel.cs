using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class MessageModel
    {
        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        [MaxLength(100, ErrorMessage = "Maximum 100 karakter içermelidir")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        [MaxLength(100, ErrorMessage = "Maximum 100 karakter içermelidir")]
        public string UserMail { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(2, ErrorMessage = "Minimum 2 karakterli olmalıdır")]
        [MaxLength(50, ErrorMessage = "Maximum 50 karakter içermelidir")]
        public string Topic { get; set; }

        [Required(ErrorMessage = "Zorunlu alandır")]
        [MinLength(10, ErrorMessage = "Minimum 10 karakterli olmalıdır")]
        [MaxLength(500, ErrorMessage = "Maximum 500 karakter içermelidir")]
        public string Content { get; set; }
    }
}
