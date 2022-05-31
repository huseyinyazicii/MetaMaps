using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Core.Utilities.Helpers;
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
    public class CreaterController : Controller
    {
        ICreaterService _createrService;
        INotyfService _notyfService;

        public CreaterController(ICreaterService createrService, INotyfService notyfService)
        {
            _createrService = createrService;
            _notyfService = notyfService;
        }

        public IActionResult CreaterPage()
        {
            var result = _createrService.GetAll();
            return View(result.Data);
        }

        public IActionResult ChangeStatus(int id, bool status)
        {
            var result = _createrService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                result.Data.Status = !status;
                _createrService.Update(result.Data);
                if (status)
                {
                    _notyfService.Warning("Geliştirici Kaldırıldı.");
                }
                else
                {
                    _notyfService.Success("Geliştirici Eklendi.");
                }
            }
            return RedirectToAction("CreaterPage", "Creater", new { area = "Admin" });
        }

        public IActionResult Delete(int id)
        {
            var result = _createrService.Delete(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                _notyfService.Success(result.Message);
            }
            return RedirectToAction("CreaterPage", "Creater", new { area = "Admin" });
        }

        public IActionResult Add()
        {
            var model = new CreaterModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(CreaterModel creater)
        {
            if (!ModelState.IsValid)
            {
                return View(creater);
            }
            var newCreater = new Creater
            {
                Name = creater.Name,
                Surname = creater.Surname,
                Email = creater.Email,
                Image = creater.Image == null ? "default.png" : ImageFile.Add(creater.Image, "wwwroot/Images/Creaters/"),
                Details = creater.Details,
                Github = creater.Github,
                Linkedin = creater.Linkedin,
                Youtube = creater.Youtube,
                Instagram = creater.Instagram,
                Twitter = creater.Twitter,
                Status = true
            };
            var result = _createrService.Add(newCreater);
            _notyfService.Success(result.Message);
            return RedirectToAction("CreaterPage", "Creater", new { area = "Admin" });
        }

        public IActionResult Update(int id)
        {
            var result = _createrService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
                return RedirectToAction("CreaterPage", "Creater", new { area = "Admin" });
            }
            var model = new CreaterModel
            {
                Id = id,
                Name = result.Data.Name,
                Surname = result.Data.Surname,
                Email = result.Data.Email,
                Details = result.Data.Details,
                Github = result.Data.Github,
                Linkedin = result.Data.Linkedin,
                Youtube = result.Data.Youtube,
                Instagram = result.Data.Instagram,
                Twitter = result.Data.Twitter
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CreaterModel creater)
        {
            if (!ModelState.IsValid)
            {
                return View(creater);
            }

            var updateCreater = _createrService.GetById(creater.Id);
            updateCreater.Data.Name = creater.Name;
            updateCreater.Data.Surname = creater.Surname;
            updateCreater.Data.Email = creater.Email;
            updateCreater.Data.Details = creater.Details;
            updateCreater.Data.Github = creater.Github;
            updateCreater.Data.Linkedin = creater.Linkedin;
            updateCreater.Data.Youtube = creater.Youtube;
            updateCreater.Data.Instagram = creater.Instagram;
            updateCreater.Data.Twitter = creater.Twitter;
            updateCreater.Data.Image = creater.Image == null ? updateCreater.Data.Image : ImageFile.Add(creater.Image, "wwwroot/Images/Creaters/");

            var result = _createrService.Update(updateCreater.Data);
            _notyfService.Success(result.Message);
            return RedirectToAction("CreaterPage", "Creater", new { area = "Admin" });
        }
    }
}
