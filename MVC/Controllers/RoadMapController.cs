using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class RoadMapController : Controller
    {
        IRoadMapService _roadMapService;

        public RoadMapController(IRoadMapService roadMapService)
        {
            _roadMapService = roadMapService;
        }

        public IActionResult RoadMaps()
        {
            var model = _roadMapService.GetByStatus(true).Data;
            return View(model);
        }

        public IActionResult RoadMapDetail(int id)
        {
            var model = _roadMapService.GetByDetails(id).Data;

            return View(model);
        }
    }
}
