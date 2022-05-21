using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICommentService
    {
        IResult Add(Comment comment);
        IResult Delete(int id);
        IDataResult<List<Comment>> GetBySourceId(int sourceId);
    }
}
