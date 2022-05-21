using Business.Abstract;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.ViewComponents
{
    public class NavbarViewComponent : ViewComponent
    {
        IMessageService _messageService;

        public NavbarViewComponent(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public IViewComponentResult Invoke()
        {
            var model = _messageService.NewMessageCount().Data;
            return View(model);
        }
    }
}
