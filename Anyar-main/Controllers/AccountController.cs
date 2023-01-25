using Exam2.Models;
using Exam2.Models.ViewModels.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam2.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signIngManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signIngManager)
        {
            _userManager = userManager;
            _signIngManager = signIngManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM registerVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(registerVM.UserName);
            if (user != null)
            {
                ModelState.AddModelError(" ", " Bu istifadechi artiq movcuddur");
            }
            user = new AppUser 
            {
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                LastName=registerVM.Surname,
                FirstName=registerVM.Name
            };
            IdentityResult result = await _userManager.CreateAsync(user,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach ( var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _signIngManager.SignInAsync(user, isPersistent: true);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError(" ", "Login or Password wrong");
                    return View();
                }
            }
            var result = await _signIngManager.PasswordSignInAsync(user, loginVM.Password, loginVM.IsPersistance, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(" ", "Login or Password wrong");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
