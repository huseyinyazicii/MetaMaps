using Core.Entities;
using Entities.Concrete;
using System.Collections.Generic;

namespace Entities.DTOs
{
    public class RoadMapDetailDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<StepOfBranchesDto> Steps { get; set; }
    }
}
