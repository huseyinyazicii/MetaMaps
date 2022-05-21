using Core.Entities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class StepOfBranchesDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
