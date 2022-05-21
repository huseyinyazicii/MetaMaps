using Business.Abstract;
using Business.Constant;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AnnouncementManager : IAnnouncementService
    {
        IAnnouncementDal _announcementDal;

        public AnnouncementManager(IAnnouncementDal announcementDal)
        {
            _announcementDal = announcementDal;
        }

        [ValidationAspect(typeof(AnnouncementValidator))]
        public IResult Add(Announcement announcement)
        {
            _announcementDal.Add(announcement);
            return new SuccessResult();
        }

        public IResult Delete(int id)
        {
            var announcement = _announcementDal.Get(x => x.Id == id);
            if(announcement == null)
            {
                return new ErrorResult(Messages.GetErrorAnnouncement);
            }
            _announcementDal.Delete(announcement);
            return new SuccessResult();
        }

        public IDataResult<Announcement> GetById(int id)
        {
            var result = _announcementDal.Get(x => x.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<Announcement>(Messages.GetErrorAnnouncement);
            }
            return new SuccessDataResult<Announcement>(result);
        }

        public IDataResult<List<Announcement>> GetByStatus(bool status)
        {
            var result = _announcementDal.GetAll(x => x.Status == status);
            return new SuccessDataResult<List<Announcement>>(result);
        }

        [ValidationAspect(typeof(AnnouncementValidator))]
        public IResult Update(Announcement announcement)
        {
            _announcementDal.Update(announcement);
            return new SuccessResult();
        }
    }
}
