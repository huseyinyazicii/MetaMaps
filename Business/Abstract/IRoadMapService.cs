using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IRoadMapService
    {
        IDataResult<List<RoadMap>> GetByStatus(bool status);
        IDataResult<RoadMap> GetById(int id);
        IDataResult<RoadMap> GetLast();
        IResult Add(RoadMap roadMap);
        IResult Delete(int id);
        IResult Update(RoadMapDetailDto roadMap);
        IDataResult<RoadMapDetailDto> GetByDetails(int roadMapId);
    }
}
