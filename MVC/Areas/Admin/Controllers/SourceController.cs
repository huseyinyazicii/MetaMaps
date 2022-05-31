using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class SourceController : Controller
    {
        ISourceService _sourceService;
        INotyfService _notyfService;
        ICommentService _commentService;

        public SourceController(ISourceService sourceService, INotyfService notyfService, ICommentService commentService)
        {
            _sourceService = sourceService;
            _notyfService = notyfService;
            _commentService = commentService;
        }

        public IActionResult SourcePage(int id)
        {
            var model = _sourceService.GetByBranchIdAndStatus(id, true).Data;
            if(model.Count <= 0)
            {
                _notyfService.Error("Bu dalın kaynağı bulunmamamktadır");
                return RedirectToAction("BranchPage", "Branch");
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            _sourceService.Delete(id);
            _notyfService.Success("Kaynak silindi");
            return RedirectToAction("BranchPage", "Branch");
        }

        public IActionResult DeleteComment(int id)
        {
            _commentService.Delete(id);
            _notyfService.Success("Yorum silindi");
            return RedirectToAction("BranchPage", "Branch");
        }

        public IActionResult Comments(int id)
        {
            var model = _commentService.GetBySourceId(id).Data;
            if(model.Count <= 0)
            {
                _notyfService.Error("Bu kaynağın yorumu bulunmamaktadır");
                return RedirectToAction("BranchPage", "Branch");
            }
            return View(model);
        }
    }
}
