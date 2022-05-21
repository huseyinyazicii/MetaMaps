using Entities.Concrete;
using System.Collections.Generic;

namespace MVC.Models
{
    public class ContactModel
    {
        public MessageModel Message { get; set; }
        public List<Creater> Creaters { get; set; }
    }
}
