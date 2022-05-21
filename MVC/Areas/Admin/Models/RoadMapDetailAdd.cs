using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Models
{
    public class RoadMapDetailAdd
    {
        public int Id { get; set; }
        public int[] BranchOfStep { get; set; }
        public int[] Steps { get; set; }
    }
}
