using Core.Utilities.Results;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IMessageService
    {
        IDataResult<List<Message>> GetByStatus(bool status);
        IDataResult<Message> GetById(int id);
        IResult Add(Message message);
        IResult Update(Message message);
        IResult Delete(int id);
        IDataResult<int> NewMessageCount();
    }
}
