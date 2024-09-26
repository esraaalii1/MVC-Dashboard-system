using DemoDataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoPresentationLayer.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly RoleManager<IdentityRole> _roleManger;

        public RoleController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            _userManger = userManager;
            _roleManger = roleManager;
        }

        public async Task<IActionResult> Index(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var roles = await _roleManger.Roles.Select(u => new RoleVM
                {
                    
                    Id = u.Id,
                   Name=u.Name
                }).ToListAsync();
                return View(roles);

            }
            var role = await _roleManger.FindByNameAsync(name);
            if (role is null) return View(Enumerable.Empty<RoleVM>());
            var model = new RoleVM
            {
                Id = role.Id,
                Name = role.Name

            };
            return View(model);
        }

        public async Task<IActionResult> Create(){ return View(); }
        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model) {
            if(!ModelState.IsValid)return View(model);
            var role = new IdentityRole
            {
                Name = model.Name
            };
            var result = await _roleManger.CreateAsync(role);
            if(result.Succeeded) return RedirectToAction("Index");
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(model);
        }


        public async Task<IActionResult> Details(string id, string viewName = nameof(Details))
        {
            if (string.IsNullOrWhiteSpace(id)) return BadRequest();
            var role = await _roleManger.FindByIdAsync(id);
            if (role is null) return View(Enumerable.Empty<RoleVM>());
            var model = new RoleVM
            {
                Id = role.Id,
                Name = role.Name

            };
            return View(viewName, model);

        }

        public async Task<IActionResult> Edit(string id) => await Details(id, nameof(Edit));
        [HttpPost]
        public async Task<IActionResult> Edit(string id, RoleVM model)
        {
            if (id != model.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var role = await _roleManger.FindByIdAsync(model.Id);
                    if (role is null) return NotFound();
                    role.Name = model.Name;
                    await _roleManger.UpdateAsync(role);

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
                var role = await _roleManger.FindByIdAsync(id);
                if (role is null) return NotFound();
                await _roleManger.DeleteAsync(role);

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
