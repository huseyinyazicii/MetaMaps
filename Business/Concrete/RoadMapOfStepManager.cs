using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class RoadMapOfStepManager : IRoadMapOfStepService
    {
        IRoadMapOfStepDal _roadMapOfStepDal;

        public RoadMapOfStepManager(IRoadMapOfStepDal roadMapOfStepDal)
        {
            _roadMapOfStepDal = roadMapOfStepDal;
        }

        public IResult Add(RoadMapOfStep roadMapOfStep)
        {
            _roadMapOfStepDal.Add(roadMapOfStep);
            return new SuccessResult();
        }
    }
}
