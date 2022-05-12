using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentManagementV2.Api.ViewModels;
using System;
using System.Threading.Tasks;

namespace StudentManagementV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> _userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> _roleManager)
        {
            this._userManager = _userManager;
            this._signInManager = signInManager;
            this._roleManager = _roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            var model = new LoginViewModel();
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //if (ModelState.IsValid)
            //{
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("loginFail", "Invalid login attempt.");

                    return View(model);
                }
            //}
            //return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register(string returnUrl)
        {
            var model = new RegisterViewModel();
            model.ReturnUrl = returnUrl;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model, string role)
        {

            if (String.IsNullOrEmpty(role))
            {
                ModelState.AddModelError("UnspecifiedRole", "Role is required");

                return View(model);
            }

            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Add role
                    IdentityRole assignedRole = await _roleManager.FindByNameAsync(role);
                    
                    if (assignedRole != null)
                    {
                        await _userManager.AddToRoleAsync(user, assignedRole.Name);
                    }

                    if (!this.User.Identity.IsAuthenticated)
                    {
                        // Login after register
                        await _signInManager.SignInAsync(user, isPersistent: false);
                    }

                    // Redirect to Create User Page (Student/Admin)
                    switch(role)
                    {
                        case "Student":
                            StudentViewModel studentViewModel = new StudentViewModel();
                            studentViewModel.IdentityId = user.Id;
                            studentViewModel.ReturnUrl = model.ReturnUrl;
                            return RedirectToAction("CreateNewStudent", "Student", studentViewModel);
                            
                        case "Admin":
                            break;

                        default:
                            break;
                    }

                    if (!string.IsNullOrEmpty(model.ReturnUrl))
                        return Redirect(model.ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return RedirectToAction("Register");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied ()
        {
            return View();
        }

        [Consumes("application/x-www-form-urlencoded")]
        [HttpPost]
        public async Task<JsonResult> ValidateNewAccount ([FromForm] RegisterViewModel model)
        {
            IdentityUser user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                return new JsonResult( new { isEmailExisted = true } );
            }

            return new JsonResult( new { isSuccess = true });
        }

    }
}
