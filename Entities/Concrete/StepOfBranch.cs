using Core.Entities;

namespace Entities.Concrete
{
    public class StepOfBranch : IEntity
    {
        public int Id { get; set; }
        public int BranchId { get; set; }
        public int StepId { get; set; }
        public int RoadMapId { get; set; }
    }
}
