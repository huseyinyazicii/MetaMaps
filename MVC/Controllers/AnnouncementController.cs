using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class AnnouncementController : Controller
    {
        IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        public IActionResult AnnouncementPage()
        {
            var model = _announcementService.GetByStatus(true).Data;
            model.Reverse();
            return View(model);
        }
    }
}
