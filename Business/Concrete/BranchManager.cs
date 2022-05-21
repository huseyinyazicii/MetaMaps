using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validaton;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BranchManager : IBranchService
    {
        IBranchDal _branchDal;

        public BranchManager(IBranchDal branchDal)
        {
            _branchDal = branchDal;
        }

        [ValidationAspect(typeof(BranchValidator))]
        public IResult Add(Branch branch)
        {
            _branchDal.Add(branch);
            return new SuccessResult(Messages.AddBranch);
        }

        public IResult Delete(int id)
        {
            var result = _branchDal.Get(x => x.Id == id);
            if(result == null)
            {
                return new ErrorResult(Messages.GetErrorBranch);
            }
            _branchDal.Delete(result);
            return new SuccessResult(Messages.DeleteBranch);
        }

        public IDataResult<List<Branch>> GetAll()
        {
            var result = _branchDal.GetAll(x => x.Status == true);
            return new SuccessDataResult<List<Branch>>(result);
        }

        public IDataResult<Branch> GetById(int id)
        {
            var result = _branchDal.Get(x => x.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Branch>(Messages.GetErrorBranch);
            }
            return new SuccessDataResult<Branch>(result);
        }

        public IDataResult<List<Branch>> GetByStatus(bool status)
        {
            var result = _branchDal.GetAll(x => x.Status == status);
            return new SuccessDataResult<List<Branch>>(result);
        }

        [ValidationAspect(typeof(BranchValidator))]
        public IResult Update(Branch branch)
        {
            _branchDal.Update(branch);
            return new SuccessResult(Messages.UpdateBranch);
        }
    }
}
