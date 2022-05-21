using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class SourceManager : ISourceService
    {
        ISourceDal _sourceDal;

        public SourceManager(ISourceDal sourceDal)
        {
            _sourceDal = sourceDal;
        }

        [ValidationAspect(typeof(SourceValidator))]
        public IResult Add(Source source)
        {
            _sourceDal.Add(source);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var source = _sourceDal.Get(x => x.Id == id);
            source.Status = false;
            _sourceDal.Update(source);
            return new SuccessResult();
        }

        public IDataResult<List<SourceDetailsDto>> GetByBranchId(int branchId)
        {
            var result = _sourceDal.ResourcesDetails(branchId);
            return new SuccessDataResult<List<SourceDetailsDto>>(result);
        }

        public IDataResult<List<Source>> GetByBranchIdAndStatus(int branchId, bool status)
        {
            var result = _sourceDal.GetAll(x => x.Status == status && x.BranchId == branchId);
            return new SuccessDataResult<List<Source>>(result);
        }

        public IDataResult<List<SourceDetailsDto>> GetByBranchIdOrderByCommentCount(int branchId)
        {
            var result = _sourceDal.ResourcesDetails(branchId).OrderByDescending(x => x.CommentsCount).ToList();
            return new SuccessDataResult<List<SourceDetailsDto>>(result);
        }

        public IDataResult<List<SourceDetailsDto>> GetByBranchIdOrderByDate(int branchId)
        {
            var result = _sourceDal.ResourcesDetails(branchId).OrderByDescending(x => x.Date).ToList();
            return new SuccessDataResult<List<SourceDetailsDto>>(result);
        }

        public IDataResult<List<SourceDetailsDto>> GetByBranchIdOrderByLikeCount(int branchId)
        {
            var result = _sourceDal.ResourcesDetails(branchId).OrderByDescending(x => x.LikesCount).ToList();
            return new SuccessDataResult<List<SourceDetailsDto>>(result);
        }

        public IDataResult<List<Source>> GetByStatus(bool status)
        {
            var result = _sourceDal.GetAll(x => x.Status == status);
            return new SuccessDataResult<List<Source>>(result);
        }

        public IResult LikeIncrease(int sourceId)
        {
            var source = _sourceDal.Get(x => x.Id == sourceId);
            if(source == null)
            {
                return new ErrorResult("Kaynak bulunamadı!");
            }
            source.LikesCount++;
            _sourceDal.Update(source);
            return new SuccessResult("Beğeni atıldı");
        }
    }
}
