using AspNetCoreHero.ToastNotification.Abstractions;
using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Identity;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class SourceController : Controller
    {
        ISourceService _sourceService;
        ICommentService _commentService;
        INotyfService _notyfService;
        UserManager<User> _userManager;

        public SourceController(ISourceService sourceService, ICommentService commentService, INotyfService notyfService, UserManager<User> userManager)
        {
            _sourceService = sourceService;
            _commentService = commentService;
            _notyfService = notyfService;
            _userManager = userManager;
        }

        public IActionResult SourcePage(int id, int order=0)
        {
            List<SourceDetailsDto> model;
            if (order == 0)
            {
                model = _sourceService.GetByBranchId(id).Data;
            }
            else if(order == 1)
            {
                model = _sourceService.GetByBranchIdOrderByDate(id).Data;
            }
            else if(order == 2)
            {
                model = _sourceService.GetByBranchIdOrderByLikeCount(id).Data;
            }
            else
            {
                model = _sourceService.GetByBranchIdOrderByCommentCount(id).Data;
            }

            ViewBag.branchId = id;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(int sourceId, string comment, int branchId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _notyfService.Warning("Yorum yapmak için giriş yapmanız gerekmektedir.");
                return RedirectToAction("Login", "Security");
            }
            if(comment == null)
            {
                _notyfService.Error("Yorumunuz boş bırakılamaz");
            }
            else if(comment.Length <= 5)
            {
                _notyfService.Error("Yorumunuz en az 6 karakter içermelidir.");
            }
            else if(comment.Length > 500)
            {
                _notyfService.Error("Yorumunuz en fazla 500 karakter içermelidir.");
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var newComment = new Comment
                {
                    SourceId = sourceId,
                    Date = DateTime.Now,
                    Status = true,
                    UserId = user.Id,
                    Content = comment
                };

                _commentService.Add(newComment);
                _notyfService.Success("Yorum yapıldı");
            }
            return RedirectToAction("SourcePage", "Source", new { id = branchId });
        }

        [HttpPost]
        public async Task<IActionResult> AddSource(string content, string link, int branchId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                _notyfService.Warning("Kaynak paylaşmak için giriş yapmanız gerekmektedir.");
                return RedirectToAction("Login", "Security");
            }
            if (content == null || link == null)
            {
                _notyfService.Error("Kaynak bilgileri boş bırakılamaz");
            }
            else if (content.Length <= 5)
            {
                _notyfService.Error("Kaynak içeriği en az 6 karakter içermelidir.");
            }
            else if (content.Length > 500)
            {
                _notyfService.Error("Kaynak içeriği en fazla 500 karakter içermelidir.");
            }
            else
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                var newSource = new Source
                {
                    UserId = user.Id,
                    Status = true,
                    BranchId = branchId,
                    CommentsCount = 0,
                    LikesCount = 0,
                    Content = content,
                    Date = DateTime.Now,
                    Link = link
                };

                _sourceService.Add(newSource);
                _notyfService.Success("Kaynak eklendi");
            }
            return RedirectToAction("SourcePage", "Source", new { id = branchId });
        }

        public IActionResult LikeIncrease(int sourceId, int branchId)
        {
            var check = Request.Cookies["like"];
            if (check == sourceId.ToString())
            {
                _notyfService.Warning("Kaynak zaten beğenilmiş");
            }
            else
            {
                var result = _sourceService.LikeIncrease(sourceId);
                if (!result.Success)
                {
                    _notyfService.Error(result.Message);
                }
                else
                {
                    CookieOptions cookie = new CookieOptions();
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Append("like", sourceId.ToString(), cookie);
                    _notyfService.Success(result.Message);
                }
            }
            return RedirectToAction("SourcePage", "Source", new { id = branchId });
        }
    }
}
