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
    public class MessageController : Controller
    {
        IMessageService _messageService;
        INotyfService _notyfService;

        public MessageController(IMessageService messageService, INotyfService notyfService)
        {
            _messageService = messageService;
            _notyfService = notyfService;
        }

        public IActionResult MessagePage()
        {
            var model = _messageService.GetByStatus(true).Data;
            return View(model);
        }

        public IActionResult Old()
        {
            var model = _messageService.GetByStatus(false).Data;
            return View(model);
        }

        public IActionResult ChangeStatus(int id)
        {
            var result = _messageService.GetById(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                result.Data.Status = false;
                _messageService.Update(result.Data);
                _notyfService.Warning("Mesaj Kaldırıldı.");
            }
            return RedirectToAction("MessagePage", "Message", new { area = "Admin" });
        }

        public IActionResult Delete(int id)
        {
            var result = _messageService.Delete(id);
            if (!result.Success)
            {
                _notyfService.Error(result.Message);
            }
            else
            {
                _notyfService.Success(result.Message);
            }
            return RedirectToAction("Old", "Message", new { area = "Admin" });
        }
    }
}
