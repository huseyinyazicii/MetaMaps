using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CommentDto : IDto
    {
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserImage { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
