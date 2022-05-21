using System.ComponentModel.DataAnnotations;

namespace MVC.Identity
{
    public class RoleModel
    {
        [Required]
        public string Name { get; set; }
    }
}
