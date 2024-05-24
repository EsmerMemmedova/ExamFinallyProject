using Data.DAL;
using Exam_Mvcproject.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam_Mvcproject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppDbContext> _userManager;
        private readonly SignInManager<AppDbContext> _signInManager;
        private readonly RoleManager<AppDbContext> _roleManager;

        public AccountController(UserManager<AppDbContext> userManager, SignInManager<AppDbContext> signInManager, RoleManager<AppDbContext> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        //public async Task<IActionResult> CreateRoles()
        //{
        //    IdentityRole role = new IdentityRole("Admin");
        //    IdentityRole role1 = new IdentityRole("Member");
        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role1);
        //    return Ok();
        //}
        //public  async Task<IActionResult> CreateAdmin()
        //{
        //    var admin =
        //}
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(AdminLoginVM adminLoginVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            var admin = await _userManager.FindByNameAsync(adminLoginVM.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("", "UserName or Password is not Correct");
                return View();
            }
            var check=await _userManager.CheckPasswordAsync(admin,adminLoginVM.Password);
            if (!check)
            {
                ModelState.AddModelError("", "UserName or Password is not Correct");
            }
            var result = await _signInManager.PasswordSignInAsync(admin, adminLoginVM.Password, false, false);
            return RedirectToAction(nameof(Index), "Dasboard");
        }
    }
}
