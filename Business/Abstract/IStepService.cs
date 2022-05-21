using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IStepService
    {
        IDataResult<List<Step>> GetByStatus(bool status);
        IDataResult<Step> GetById(int id);
        IResult Add(Step step);
        IResult Update(Step step);
    }
}
