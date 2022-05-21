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
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        [ValidationAspect(typeof(MessageValidator))]
        public IResult Add(Message message)
        {
            _messageDal.Add(message);
            return new SuccessResult(Messages.AddMessage);
        }

        public IResult Delete(int id)
        {
            var message = _messageDal.Get(x => x.Id == id);
            if(message == null)
            {
                return new ErrorResult(Messages.GetErrorMessage);
            }
            _messageDal.Delete(message);
            return new SuccessResult(Messages.DeleteMessage);
        }

        public IDataResult<Message> GetById(int id)
        {
            var message = _messageDal.Get(x => x.Id == id);
            if (message == null)
            {
                return new ErrorDataResult<Message>(Messages.GetErrorMessage);
            }
            return new SuccessDataResult<Message>(message);
        }

        public IDataResult<List<Message>> GetByStatus(bool status)
        {
            var result = _messageDal.GetAll(x => x.Status == status);
            return new SuccessDataResult<List<Message>>(result);
        }

        public IDataResult<int> NewMessageCount()
        {
            return new SuccessDataResult<int>(_messageDal.NewMessageCount());
        }

        public IResult Update(Message message)
        {
            _messageDal.Update(message);
            return new SuccessResult(Messages.UpdateMessage);
        }
    }
}
