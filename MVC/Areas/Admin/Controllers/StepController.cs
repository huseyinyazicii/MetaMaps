using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Areas.Admin.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class StepController : Controller
    {
        IStepService _stepService;
        INotyfService _notyfService;

        public StepController(IStepService stepService, INotyfService notyfService)
        {
            _stepService = stepService;
            _notyfService = notyfService;
        }

        public IActionResult GetAll()
        {
            var steps = JsonConvert.SerializeObject(_stepService.GetByStatus(true).Data);
            return Json(steps);
        }

        public IActionResult StepPage()
        {
            var model = _stepService.GetByStatus(true).Data;
            return View(model);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(StepModel stepModel)
        {
            if (!ModelState.IsValid)
            {
                return View(stepModel);
            }
            var step = new Step
            {
                Status = true,
                Name = stepModel.Name
            };
            _stepService.Add(step);
            _notyfService.Success("Adım eklendi");
            return RedirectToAction("StepPage", "Step");
        }

        public IActionResult Update(int id)
        {
            var step = _stepService.GetById(id).Data;
            var model = new StepModel
            {
                Id = step.Id,
                Name = step.Name
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(StepModel stepModel)
        {
            if (!ModelState.IsValid)
            {
                return View(stepModel);
            }
            var step = _stepService.GetById(stepModel.Id).Data;
            step.Name = stepModel.Name;
            _stepService.Update(step);
            _notyfService.Success("Adım güncellendi");
            return RedirectToAction("StepPage", "Step");
        }

        public IActionResult Delete(int id)
        {
            var step = _stepService.GetById(id).Data;
            step.Status = false;
            _stepService.Update(step);
            _notyfService.Success("Adım silindi");
            return RedirectToAction("StepPage", "Step");
        }
    }
}
