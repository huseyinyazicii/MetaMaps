using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class ContactController : Controller
    {
        ICreaterService _createrService;
        INotyfService _notyfService;
        IMessageService _messageService;

        public ContactController(ICreaterService createrService, INotyfService notyfService, IMessageService messageService)
        {
            _createrService = createrService;
            _notyfService = notyfService;
            _messageService = messageService;
        }

        public IActionResult ContactPage()
        {
            var model = new ContactModel
            {
                Creaters = _createrService.GetAll().Data,
                Message = new MessageModel()
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult ContactPage(ContactModel contact)
        {
            if (!ModelState.IsValid)
            {
                var model = new ContactModel
                {
                    Creaters = _createrService.GetAll().Data,
                    Message = contact.Message
                };
                return View(model);
            }
            var message = new Message
            {
                Status = true,
                Content = contact.Message.Content,
                Date = DateTime.Now,
                Topic = contact.Message.Topic,
                UserMail = contact.Message.UserMail,
                UserName = contact.Message.UserName
            };

            _messageService.Add(message);
            _notyfService.Success("Mesajınız gönderildi");
            return RedirectToAction("ContactPage", "Contact");
        }
    }
}
