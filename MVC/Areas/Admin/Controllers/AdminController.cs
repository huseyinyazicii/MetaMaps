using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")] // (Roles = "admin")
    public class AdminController : Controller
    {
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public AdminController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult RoleList()
        {
            return View(_roleManager.Roles);
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleModel);
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleModel.Name));
            if (!result.Succeeded)
            {
                throw new ApplicationException("Rol oluşturulurken bir hata oluştu");
            }
            return RedirectToAction("RoleList", "Admin", new { area = "Admin" });
        }

        public async Task<IActionResult> RoleEdit(string  id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<User>();
            var nonMembers = new List<User>();

            foreach (var user in _userManager.Users.ToList())
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }

            var model = new RoleDetails()
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RoleEdit(RoleEditModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model.RoleId);
            }

            foreach (var userId in model.IdsToAdd ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if(user  != null)
                {
                    var result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Rol düzenlenirken bir hata oluştu");
                    }
                }
            }

            foreach (var userId in model.IdsToDelete ?? new string[] { })
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Rol düzenlenirken bir hata oluştu");
                    }
                }
            }

            return RedirectToAction("RoleList", "Admin", new { area = "Admin" });
        }

        public IActionResult UserList()
        {
            return View(_userManager.Users);  //ToList()
        }

        public async Task<IActionResult> UserEdit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null)
            {
                var selectedRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.Select(i => i.Name);

                ViewBag.Roles = roles;

                return View(new UserDetailsModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Name = user.Name,
                    Surname = user.Surname,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    SelectedRoles = selectedRoles,
                });
            }
            return RedirectToAction("UserList", "Admin", new { area = "Admin" });
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserDetailsModel model, string[] selectedRoles)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(model.UserId);
            if(user != null)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.EmailConfirmed = model.EmailConfirmed;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    selectedRoles = selectedRoles ?? new string[] { };
                    await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles).ToArray<string>());
                    await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles).ToArray<string>());
                    return RedirectToAction("UserList", "Admin", new { area = "Admin" });
                }
            }
            return RedirectToAction("UserList", "Admin", new { area = "Admin" });
        }
    }
}
