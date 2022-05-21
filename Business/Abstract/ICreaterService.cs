using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICreaterService
    {
        IDataResult<List<Creater>> GetAll();
        IDataResult<Creater> GetById(int id);
        IResult Add(Creater creater);
        IResult Update(Creater creater);
        IResult Delete(int id);
    }
}
