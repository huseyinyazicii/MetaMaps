using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSourceDal : EfEntityRepositoryBase<Source, MetaMapsContext>, ISourceDal
    {
        public List<SourceDetailsDto> ResourcesDetails(int branchId)
        {
            using (var context = new MetaMapsContext())
            {
                var result = from s in context.Sources.Where(x => x.BranchId == branchId && x.Status == true)
                             join u in context.AspNetUsers on s.UserId equals u.Id
                             select new SourceDetailsDto
                             {
                                BranchId = s.BranchId,
                                SourceId = s.Id,
                                Content = s.Content,
                                OpenComment = "a"+s.Id.ToString(),
                                Date = s.Date,
                                Link = s.Link,
                                LikesCount = s.LikesCount,
                                CommentsCount = s.CommentsCount,
                                UserImage = u.Image,
                                UserName = u.Name,
                                UserSurname = u.Surname,
                                Comments = (from c in context.Comments.Where(x => x.SourceId == s.Id && x.Status == true)
                                            join us in context.AspNetUsers on c.UserId equals us.Id
                                            select new CommentDto
                                            {
                                                UserName = us.Name,
                                                UserSurname = us.Surname,
                                                UserImage = us.Image,
                                                Content = c.Content,
                                                Date = c.Date
                                            }).ToList()
                             };
                return result.ToList();
            }
        }
    }
}
