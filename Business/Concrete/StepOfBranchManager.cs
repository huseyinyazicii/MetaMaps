using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class StepOfBranchManager : IStepOfBranchService
    {
        IStepOfBranchDal _stepOfBranchDal;

        public StepOfBranchManager(IStepOfBranchDal stepOfBranchDal)
        {
            _stepOfBranchDal = stepOfBranchDal;
        }

        public IResult Add(StepOfBranch stepOfBranch)
        {
            //var result = _stepOfBranchDal.Get(x => x.BranchId == stepOfBranch.BranchId && x.BranchId == stepOfBranch.StepId);
            //if(result == null)
            //{
            //    return new ErrorResult();
            //}
            _stepOfBranchDal.Add(stepOfBranch);
            return new SuccessResult();
        }
    }
}
