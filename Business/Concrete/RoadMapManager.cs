using Business.Abstract;
using Business.Constant;
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
    public class RoadMapManager : IRoadMapService
    {
        IRoadMapDal _roadMapDal;

        public RoadMapManager(IRoadMapDal roadMapDal)
        {
            _roadMapDal = roadMapDal;
        }

        [ValidationAspect(typeof(RoadMapValidator))]
        public IResult Add(RoadMap roadMap)
        {
            _roadMapDal.Add(roadMap);
            return new SuccessResult(Messages.AddRoadMap);
        }

        public IResult Delete(int id)
        {
            var roadMap = _roadMapDal.Get(x => x.Id == id);
            if(roadMap == null)
            {
                return new ErrorResult(Messages.GetErrorRoadMap);
            }
            roadMap.Status = false;
            _roadMapDal.Update(roadMap);
            return new SuccessResult(Messages.DeleteRoadMap);
        }

        public IDataResult<List<RoadMap>> GetByStatus(bool status)
        {
            return new SuccessDataResult<List<RoadMap>>(_roadMapDal.GetAll(x => x.Status == status));
        }

        public IDataResult<RoadMapDetailDto> GetByDetails(int roadMapId)
        {
            var result = _roadMapDal.GetRoadMapDetails(roadMapId);
            return new SuccessDataResult<RoadMapDetailDto>(result);
        }

        public IDataResult<RoadMap> GetById(int id)
        {
            var roadMap = _roadMapDal.Get(x => x.Id == id);
            if (roadMap == null)
            {
                return new ErrorDataResult<RoadMap>(Messages.GetErrorRoadMap);
            }
            return new SuccessDataResult<RoadMap>(roadMap);
        }

        public IResult Update(RoadMapDetailDto roadMap)
        {
            throw new System.NotImplementedException();
        }

        public IDataResult<RoadMap> GetLast()
        {
            return new SuccessDataResult<RoadMap>(_roadMapDal.GetLast());
        }
    }
}
