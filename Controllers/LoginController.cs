using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class LoginController : Controller
{
    public LoginController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager,
        ILogger<UserModel> logger, RoleManager<RoleModel> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
        _roleManager = roleManager;
    }

    private UserManager<UserModel> _userManager;
    private SignInManager<UserModel> _signInManager;
    private ILogger<UserModel> _logger;
    private RoleManager<RoleModel> _roleManager;


    // GET
    public IActionResult Index()
    {
        if (User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    [ActionName("Index")]
    public async Task<IActionResult> OnPostLogin(LoginModel logInfo)
    {
        if (ModelState.IsValid)
        {
            var result = await
                _signInManager.PasswordSignInAsync(logInfo.Username, logInfo.Password, logInfo.Remember,
                    lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        else
        {
            return View();
        }

        return View();
    }
}