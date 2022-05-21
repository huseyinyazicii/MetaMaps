using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        IRoadMapService _roadMapService;
        public HomeController(IRoadMapService roadMapService, ICreaterService createrService)
        {
            _roadMapService = roadMapService;
        }

        public IActionResult HomePage()
        {
            var roadMaps = _roadMapService.GetByStatus(true).Data;

            var model = new HomePageModel()
            {
                RoadMaps = roadMaps
            };

            return View(model);
        }

        public IActionResult ErrorPage(int code)
        {
            return View();
        }
    }
}
