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
    public class BranchController : Controller
    {
        IBranchService _branchService;
        INotyfService _notyfService;

        public BranchController(IBranchService branchService, INotyfService notyfService)
        {
            _branchService = branchService;
            _notyfService = notyfService;
        }

        public IActionResult BranchPage()
        {
            var result = _branchService.GetAll();
            return View(result.Data);
        }

        public IActionResult ChangeStatus(int id, bool status)
        {
            var result = _branchService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                result.Data.Status = !status;
                _branchService.Update(result.Data);
                if (status)
                {
                    _notyfService.Warning("Dal Kaldırıldı.");
                }
                else
                {
                    _notyfService.Success("Dal Eklendi.");
                }
            }
            return RedirectToAction("BranchPage", "Branch", new { area = "Admin" });
        }

        //public IActionResult Delete(int id)
        //{
        //    var result = _branchService.Delete(id);
        //    if (!result.Success)
        //    {
        //        _notyfService.Error(result.Message);
        //    }
        //    else
        //    {
        //        _notyfService.Success(result.Message);
        //    }
        //    return RedirectToAction("BranchPage", "Branch", new { area = "Admin" });
        //}

        public IActionResult Add()
        {
            var model = new BranchModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(BranchModel branch)
        {
            if (!ModelState.IsValid)
            {
                return View(branch);
            }
            var newBranch = new Branch
            {
                Name = branch.Name,
                Status = true
            };
            var result = _branchService.Add(newBranch);
            _notyfService.Success(result.Message);
            return RedirectToAction("BranchPage", "Branch", new { area = "Admin" });
        }

        public IActionResult Update(int id)
        {
            var result = _branchService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
                return RedirectToAction("BranchPage", "Branch", new { area = "Admin" });
            }
            var model = new BranchModel
            {
                Id = id,
                Name = result.Data.Name
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Update(BranchModel branch)
        {
            if (!ModelState.IsValid)
            {
                return View(branch);
            }

            var updateBranch = _branchService.GetById(branch.Id);
            updateBranch.Data.Name = branch.Name;

            var result = _branchService.Update(updateBranch.Data);
            _notyfService.Success(result.Message);
            return RedirectToAction("BranchPage", "Branch", new { area = "Admin" });
        }
    }
}
