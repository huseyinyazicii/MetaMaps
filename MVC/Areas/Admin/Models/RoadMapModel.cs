using Entities.Concrete;
using System.Collections.Generic;

namespace MVC.Areas.Admin.Models
{
    public class RoadMapModel
    {
        public List<Step> Steps { get; set; }
        public List<Branch> Branches { get; set; }
        public int RoadMapId { get; set; }
    }
}