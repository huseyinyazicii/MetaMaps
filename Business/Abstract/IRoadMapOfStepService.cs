using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRoadMapOfStepService
    {
        IResult Add(RoadMapOfStep roadMapOfStep);
    }
}
