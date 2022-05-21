using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMessageDal : EfEntityRepositoryBase<Message, MetaMapsContext>, IMessageDal
    {
        public int NewMessageCount()
        {
            using (var context = new MetaMapsContext())
            {
                return context.Set<Message>().Where(x => x.Status == true).Count();
            }
        }
    }
}
