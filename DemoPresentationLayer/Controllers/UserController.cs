global using DemoPresentationLayer.ViewModels;
using DemoBusinessLayer.Interfaces;
using DemoDataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DemoBusinessLayer.Repositories;
using DemoPresentationLayer.Utilities;

namespace DemoPresentationLayer.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;

		public UserController(UserManager<ApplicationUser> userManager)
		{
            _userManger = userManager;
		}

		public async Task<IActionResult> Index(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                var users = await _userManger.Users.Select(u => new UserVM
                {
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Id = u.Id,
                    UserName = u.UserName,
                    Roles = _userManger.GetRolesAsync(u).GetAwaiter().GetResult()
                }).ToListAsync();
                return View(users);
                
            }
            var user= await _userManger.FindByEmailAsync(email);
            if (user is null) return View(Enumerable.Empty<UserVM>());
            var model = new UserVM
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                Roles = _userManger.GetRolesAsync(user).GetAwaiter().GetResult()

            };
            return View(model);
        }
        public async Task<IActionResult> Details(string id,string viewName =nameof(Details))
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var user = await _userManger.FindByIdAsync(id);
            if (user is null) return NotFound();
            var usermodel = new UserVM
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = user.Id,
                UserName = user.UserName,
                Roles = await _userManger.GetRolesAsync(user)
            };
            return View(usermodel);

        }

        public async Task<IActionResult> Edit(string id) => await Details(id, nameof(Edit));
        [HttpPost]
        public async Task<IActionResult> Edit(string id, UserVM model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userManger.FindByEmailAsync(model.Email);
                    if (user is null) return NotFound();
                    user.FirstName=model.FirstName;
                    user.LastName=model.LastName;
                    await _userManger.UpdateAsync(user);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(model);
        }


        public async Task<IActionResult> Delete(string id) => await Details(id, nameof(Delete));
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            try
            {
                var user = await _userManger.FindByIdAsync(id);
                if (user is null) return NotFound();
                await _userManger.DeleteAsync(user);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return View();
    }


    }
}
