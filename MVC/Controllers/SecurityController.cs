using AspNetCoreHero.ToastNotification.Abstractions;
using Core.Utilities.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Identity;
using MVC.Models.Security;
using System;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [AutoValidateAntiforgeryToken]
    public class SecurityController : Controller
    {
        UserManager<User> _userManager;
        SignInManager<User> _signInManager;
        IEmailSender _emailSender;
        INotyfService _notyfService;

        public SecurityController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _notyfService = notyfService;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByEmailAsync(loginViewModel.UserMail);
            if(user != null)
            {
                if(!await _userManager.IsEmailConfirmedAsync(user))
                {
                    _notyfService.Error("Email adresinizi doğrulayın lütfen");
                    return View(loginViewModel);
                }
            }
            else
            {
                _notyfService.Error("Böyle bir kullanıcı bulunamadı");
                return View(loginViewModel);
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginViewModel.Password, false, false);
            if (!result.Succeeded)
            {
                _notyfService.Error("Hatalı Giriş");
                return View(loginViewModel);
            }

            _notyfService.Success("Hoşgeldiniz..");
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult Register() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new User
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Email,
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Image = "default.png"
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!result.Succeeded)
            {
                _notyfService.Error("Bilinmeyen bir hata oldu. Lütfen tekrar deneyiniz");
                return View(registerViewModel);
            }

            var confirmationcode = _userManager.GenerateEmailConfirmationTokenAsync(user);
            var url = Url.Action("ConfirmEmail", "Security", new { userId = user.Id, code = confirmationcode.Result });

            await _emailSender.SendEmail(registerViewModel.Email, "Hesabınızı Onaylayın",
                $"Hesabınızı onaylamak için linke <a href='https://localhost:44369{url}'>tıklayınız</a>");

            return RedirectToAction("HomePage", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult AccessDenied() // Yetkiniz Yok
        {
            return View();
        }

        public IActionResult ForgotPasswordEmailSent() // Şifremi Unuttum Mail Gönderme
        {
            return View();
        }

        public IActionResult ResetPasswordConfirm() // Şifreniz Sıfırlandı
        {
            return View();
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if(userId == null || code == null)
            {
                throw new ApplicationException("Hatalı Kod veya kullanıcı");
            }

            var user = await _userManager.FindByIdAsync(userId);

            if(user == null)
            {
                throw new ApplicationException("Kullanıcı Bulunamadı");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            if (!result.Succeeded)
            {
                throw new ApplicationException("Geçersiz Kod");
            }

            return View("ConfirmEmail");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                _notyfService.Error("Email adresinizi lütfen giriniz");
                return View();
            }

            var user = await _userManager.FindByEmailAsync(email);
            if(user == null)
            {
                _notyfService.Error("Lütfen doğru email'i giriniz");
                return View();
            }

            var confirmationCode = _userManager.GeneratePasswordResetTokenAsync(user);
            var url = Url.Action("ResetPassword", "Security",
                new { userId = user.Id, code = confirmationCode.Result });

            await _emailSender.SendEmail(email, "Reset Password",
                $"Parolanızı yenilemek için linke <a href='https://localhost:44369{url}'>tıklayınız.</a>");

            return RedirectToAction("ForgotPasswordEmailSent");
        }

        public IActionResult ResetPassword(string userId, string code)
        {
            if(userId == null || code == null)
            {
                throw new ApplicationException("User Id ve Code boş olamaz");
            }
            var model = new ResetPasswordViewModel { Code = code };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if(user == null)
            {
                throw new ApplicationException("Kullanıcı Bulunamadı");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

            if (!result.Succeeded)
            {
                return View();
            }
            return RedirectToAction("ResetPasswordConfirm");
        }
    }
}
