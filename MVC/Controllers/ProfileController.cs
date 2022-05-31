using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Identity;
using MVC.Models;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private UserManager<User> _userManager;
        INotyfService _notyfService;

        public ProfileController(UserManager<User> userManager, INotyfService notyfService)
        {
            _userManager = userManager;
            _notyfService = notyfService;
        }

        public async Task<IActionResult> Details()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if(user == null)
            {
                return RedirectToAction("AccessDenied", "Security");
            }

            var model = new ProfileModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Image = user.Image,
                Id = user.Id
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Details(ProfileModel profileModel)
        {
            if (!ModelState.IsValid)
            {
                return View(profileModel);
            }

            var user = await _userManager.FindByIdAsync(profileModel.Id);

            if (user == null)
            {
                _notyfService.Error("Kullanıcı bulunamadı");
                return View(profileModel);
            }

            user.Name = profileModel.Name;
            user.Surname = profileModel.Surname;
            user.Image = profileModel.ImagePath == null ? user.Image : ImageFile.Add(profileModel.ImagePath, "wwwroot/Images/Users/");

            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
            {
                _notyfService.Error("Bilinmeyen bir hata oluştu");
                return View(profileModel);
            }

            _notyfService.Success("Güncelleme yapıldı");
            return RedirectToAction("Details", "Profile");
        }
    }
}
