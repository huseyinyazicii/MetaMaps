using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IBranchService
    {
        IResult Add(Branch branch);
        IResult Update(Branch branch);
        IResult Delete(int id);
        IDataResult<List<Branch>> GetAll();
        IDataResult<List<Branch>> GetByStatus(bool status);
        IDataResult<Branch> GetById(int id);

    }
}
