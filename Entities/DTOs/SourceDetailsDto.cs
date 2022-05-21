using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class SourceDetailsDto : IDto
    {
        public int BranchId { get; set; }
        public int SourceId { get; set; }
        public string OpenComment { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserImage { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime Date { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
