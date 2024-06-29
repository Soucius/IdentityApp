using IdentityApp.Models;
using IdentityApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityApp.Controllers {
    public class UsersController: Controller {
        private UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index() {
            return View(_userManager.Users);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model) {
            if (ModelState.IsValid) {
                var user = new AppUser {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };

                IdentityResult result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }

                foreach (IdentityError err in result.Errors) {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);
        }
    }
}