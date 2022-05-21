using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Linq;
using System.Collections.Generic;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;

namespace Business.Concrete
{
    public class StepManager : IStepService
    {
        IStepDal _stepDal;

        public StepManager(IStepDal stepDal)
        {
            _stepDal = stepDal;
        }

        [ValidationAspect(typeof(StepValidator))]
        public IResult Add(Step step)
        {
            _stepDal.Add(step);
            return new SuccessResult();
        }

        public IDataResult<Step> GetById(int id)
        {
            return new SuccessDataResult<Step>(_stepDal.Get(x => x.Id == id));
        }

        public IDataResult<List<Step>> GetByStatus(bool status)
        {
            return new SuccessDataResult<List<Step>>(_stepDal.GetAll(x => x.Status == status));
        }

        [ValidationAspect(typeof(StepValidator))]
        public IResult Update(Step step)
        {
            _stepDal.Update(step);
            return new SuccessResult();
        }
    }
}
