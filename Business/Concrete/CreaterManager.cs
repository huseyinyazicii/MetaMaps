using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CreaterManager : ICreaterService
    {
        ICreaterDal _createrDal;

        public CreaterManager(ICreaterDal createrDal)
        {
            _createrDal = createrDal;
        }

        [ValidationAspect(typeof(CreaterValidator))]
        public IResult Add(Creater creater)
        {
            _createrDal.Add(creater);
            return new SuccessResult(Messages.AddCreater);
        }

        public IResult Delete(int id)
        {
            var creater = _createrDal.Get(x => x.Id == id);
            if(creater == null)
            {
                return new ErrorResult(Messages.GetErrorCreater);
            }
            _createrDal.Delete(creater);
            return new SuccessResult(Messages.DeleteCreater);
        }

        public IDataResult<List<Creater>> GetAll()
        {
            var result = _createrDal.GetAll();
            return new SuccessDataResult<List<Creater>>(result);
        }

        public IDataResult<Creater> GetById(int id)
        {
            var creater = _createrDal.Get(x => x.Id == id);
            if (creater == null)
            {
                return new ErrorDataResult<Creater>(Messages.GetErrorCreater);
            }
            return new SuccessDataResult<Creater>(creater);
        }

        [ValidationAspect(typeof(CreaterValidator))]
        public IResult Update(Creater creater)
        {
            _createrDal.Update(creater);
            return new SuccessResult(Messages.UpdateCreater);
        }
    }
}
