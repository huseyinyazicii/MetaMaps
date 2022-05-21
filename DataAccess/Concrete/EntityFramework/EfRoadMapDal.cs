using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRoadMapDal : EfEntityRepositoryBase<RoadMap, MetaMapsContext>, IRoadMapDal
    {
        public RoadMap GetLast()
        {
            using (var context = new MetaMapsContext())
            {
                var result = context.Set<RoadMap>().OrderBy(x => x.Id).LastOrDefault();
                return result;
            }
        }

        public RoadMapDetailDto GetRoadMapDetails(int roadMapId)
        {
            using (var context = new MetaMapsContext())
            {
                var result = from r in context.RoadMaps.Where(x => x.Id == roadMapId)
                             select new RoadMapDetailDto
                             {
                                 Id = r.Id,
                                 Description = r.Description,
                                 Name = r.Name,
                                 Image = r.Image,
                                 Steps = (from rs in context.RoadMapOfSteps
                                          join s in context.Steps on rs.StepId equals s.Id
                                          where rs.RoadMapId == r.Id
                                          select new StepOfBranchesDto
                                          {
                                              Id = s.Id,
                                              Name = s.Name,
                                              Branches = (from sb in context.StepOfBranches.Where(x => x.RoadMapId == roadMapId)
                                                          join b in context.Branches on sb.BranchId equals b.Id
                                                          where sb.StepId == s.Id
                                                          select b).ToList()
                                          }).ToList()
                             };
                return result.SingleOrDefault();
            }
        }
    }
}
