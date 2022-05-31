using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Core.Utilities.Helpers;
using Entities.Concrete;
using Entities.DTOs;
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
    public class RoadMapController : Controller
    {
        IRoadMapService _roadMapService;
        IBranchService _branchService;
        IStepService _stepService;
        IRoadMapOfStepService _roadMapOfStepService;
        IStepOfBranchService _stepOfBranchService;
        INotyfService _notyfService;

        public RoadMapController(IRoadMapService roadMapService,
                                 INotyfService notyfService,
                                 IBranchService branchService,
                                 IStepService stepService, 
                                 IStepOfBranchService stepOfBranchService, 
                                 IRoadMapOfStepService roadMapOfStepService)
        {
            _roadMapService = roadMapService;
            _notyfService = notyfService;
            _branchService = branchService;
            _stepService = stepService;
            _stepOfBranchService = stepOfBranchService;
            _roadMapOfStepService = roadMapOfStepService;
        }

        public IActionResult RoadMapPage()
        {
            var model = _roadMapService.GetByStatus(true).Data;
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _roadMapService.Delete(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                _notyfService.Success(result.Message);
            }
            return RedirectToAction("RoadMapPage", "RoadMap", new { area = "Admin" });
        }

        public IActionResult FirstAdd()
        {
            var model = new AddRoadMapModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult FirstAdd(AddRoadMapModel roadMap)
        {
            if (!ModelState.IsValid)
            {
                return View(roadMap);
            }
            var newRoadMap = new RoadMap
            {
                Name = roadMap.Name,
                Description = roadMap.Description,
                Image = roadMap.Image == null ? "default.png" : ImageFile.Add(roadMap.Image, "wwwroot/Images/RoadMaps/"),
                Status = true
            };
            _roadMapService.Add(newRoadMap);
            return RedirectToAction("SecondAdd", "RoadMap", new { area = "Admin" });
        }

        public IActionResult SecondAdd()
        {
            var model = new RoadMapModel
            {
                Steps = _stepService.GetByStatus(true).Data,
                Branches = _branchService.GetByStatus(true).Data,
                RoadMapId = _roadMapService.GetLast().Data.Id
            };
            return View(model);
        }

        public void SecondAddDetail(RoadMapDetailAdd roadMapDetail)
        {
            Array.Reverse(roadMapDetail.Steps);
            Array.Reverse(roadMapDetail.BranchOfStep);

            for (int i = 0; i < roadMapDetail.Steps.Length; i++)
            {
                var roadMapOfStep = new RoadMapOfStep
                {
                    RoadMapId = roadMapDetail.Id,
                    StepId = roadMapDetail.Steps[i]
                };
                _roadMapOfStepService.Add(roadMapOfStep);
            }

            var numberOfZero = -1;
            for (int i = 0; i < roadMapDetail.BranchOfStep.Length; i++)
            {
                if (roadMapDetail.BranchOfStep[i] == -1)
                {
                    numberOfZero++;
                    continue;
                }

                var stepOfBranch = new StepOfBranch
                {
                    StepId = roadMapDetail.Steps[numberOfZero],
                    BranchId = roadMapDetail.BranchOfStep[i],
                    RoadMapId = roadMapDetail.Id
                };
                _stepOfBranchService.Add(stepOfBranch);
            }
        }
    }
}
