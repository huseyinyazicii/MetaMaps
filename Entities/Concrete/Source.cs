using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Source : IEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BranchId { get; set; }
        public string Content { get; set; }
        public string Link { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
