using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Models
{
    public class RoadMapDetailModel
    {
        public List<Branch> Branches { get; set; }
        public List<Step> Steps { get; set; }
        public List<StepOfBranch> StepOfBranches { get; set; }
    }
}
