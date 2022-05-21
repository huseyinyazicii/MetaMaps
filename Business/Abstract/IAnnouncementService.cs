using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAnnouncementService
    {
        IDataResult<List<Announcement>> GetByStatus(bool status);
        IDataResult<Announcement> GetById(int id);
        IResult Add(Announcement announcement);
        IResult Update(Announcement announcement);
        IResult Delete(int id);
    }
}
