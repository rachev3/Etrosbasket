using Etrosbasket.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace Etrosbasket.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        { 
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return RedirectToAction("AccessDenied", "Account", new { area = "" });
            }

            // Check if the user is in the "Admin" role
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                return RedirectToAction("AccessDenied", "Account", new { area = "" });
            }

            // Attempt to sign in the user without "Remember Me"
            var result = await _signInManager.PasswordSignInAsync(user, password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                
                return RedirectToAction("Index", "AdminPanel");
            }

            if (result.IsLockedOut)
            {
                return RedirectToAction("Index", "AdminPanel");
            }

     
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "" });
        }


        public IActionResult AccessDenied()
        {
            return View();
        }
      
        //public async Task<IActionResult> GetArticlesTable()
        //{
        //    var players = await articleService.GetAll();
        //    return PartialView("_ArticlesTable", players);
        //}
    }
}
