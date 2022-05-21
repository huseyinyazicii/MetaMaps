using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ISourceService
    {
        IDataResult<List<SourceDetailsDto>> GetByBranchId(int branchId);
        IDataResult<List<Source>> GetByStatus(bool status);
        IDataResult<List<Source>> GetByBranchIdAndStatus(int branchId, bool status);
        IResult Add(Source source);
        IResult Delete(int id);
        IResult LikeIncrease(int sourceId);
        IDataResult<List<SourceDetailsDto>> GetByBranchIdOrderByLikeCount(int branchId);
        IDataResult<List<SourceDetailsDto>> GetByBranchIdOrderByCommentCount(int branchId);
        IDataResult<List<SourceDetailsDto>> GetByBranchIdOrderByDate(int branchId);
    }
}
