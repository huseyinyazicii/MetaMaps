using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class AnnouncementController : Controller
    {
        IAnnouncementService _announcementService;
        INotyfService _notyfService;

        public AnnouncementController(IAnnouncementService announcementService, INotyfService notyfService)
        {
            _announcementService = announcementService;
            _notyfService = notyfService;
        }

        public IActionResult AnnouncementPage()
        {
            var model = _announcementService.GetByStatus(true).Data;
            return View(model);
        }

        public IActionResult Old()
        {
            var model = _announcementService.GetByStatus(false).Data;
            return View(model);
        }

        public IActionResult ChangeStatus(int id, bool status)
        {
            var result = _announcementService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
                if (status)
                {
                    return RedirectToAction("Old", "Announcement", new { area = "Admin" });
                }
                return RedirectToAction("AnnouncementPage", "Announcement", new { area = "Admin" });
            }
            result.Data.Status = status;
            _announcementService.Update(result.Data);
            if (status)
            {
                _notyfService.Success("Duyuru Eklendi.");
                return RedirectToAction("Old", "Announcement", new { area = "Admin" });
            }
            _notyfService.Warning("Duyuru Kaldırıldı.");
            return RedirectToAction("AnnouncementPage", "Announcement", new { area = "Admin" });
        }

        public IActionResult Delete(int id)
        {
            var result = _announcementService.Delete(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                _notyfService.Success("Duyuru Silindi.");
            }
            return RedirectToAction("Old", "Announcement", new { area = "Admin" });
        }

        public IActionResult Add()
        {
            var model = new AnnouncementModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(AnnouncementModel announcement)
        {
            if (!ModelState.IsValid)
            {
                return View(announcement);
            }
            var newAnnouncement = new Announcement
            {
                Title = announcement.Title,
                Content = announcement.Content,
                Date = DateTime.Now,
                Status = true
            };
            _announcementService.Add(newAnnouncement);
            _notyfService.Success("Duyuru Eklendi.");
            return RedirectToAction("AnnouncementPage", "Announcement", new { area = "Admin"});
        }

        public IActionResult Update(int id)
        {
            var result = _announcementService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
                return RedirectToAction("AnnouncementPage", "Announcement", new { area = "Admin" });
            }
            var model = new AnnouncementModel
            {
                Id = id,
                Title = result.Data.Title,
                Content = result.Data.Content
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(AnnouncementModel announcement)
        {
            if (!ModelState.IsValid)
            {
                return View(announcement);
            }

            var updateAnnouncement = _announcementService.GetById(announcement.Id);
            updateAnnouncement.Data.Title = announcement.Title;
            updateAnnouncement.Data.Content = announcement.Content;

            _announcementService.Update(updateAnnouncement.Data);
            _notyfService.Success("Duyuru Güncellendi.");
            return RedirectToAction("AnnouncementPage", "Announcement", new { area = "Admin" });
        }
    }
}
