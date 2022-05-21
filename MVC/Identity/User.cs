using Microsoft.AspNetCore.Identity;

namespace MVC.Identity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
    }
}
