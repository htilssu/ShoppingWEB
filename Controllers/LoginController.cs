using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class LoginController : Controller
{
    public LoginController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager,
        ILogger<UserModel> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    private UserManager<UserModel> _userManager;
    private SignInManager<UserModel> _signInManager;
    private ILogger<UserModel> _logger;


    // GET
    public IActionResult Index()
    {
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
                ModelState.AddModelError("Fail", "Sai tên đăng nhập hoặc mật khẩu");
                return View();
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