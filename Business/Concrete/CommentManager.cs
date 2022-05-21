using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CommentManager : ICommentService
    {
        ICommentDal _commentDal;

        public CommentManager(ICommentDal commentDal)
        {
            _commentDal = commentDal;
        }

        [ValidationAspect(typeof(CommentValidator))]
        public IResult Add(Comment comment)
        {
            _commentDal.Add(comment);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var comment = _commentDal.Get(x => x.Id == id);
            if(comment == null)
            {
                return new ErrorResult("Yorum bulunamadı.");
            }
            _commentDal.Delete(comment);
            return new SuccessResult();
        }

        public IDataResult<List<Comment>> GetBySourceId(int sourceId)
        {
            var result = _commentDal.GetAll(x => x.SourceId == sourceId && x.Status == true);
            return new SuccessDataResult<List<Comment>>(result);
        }
    }
}
