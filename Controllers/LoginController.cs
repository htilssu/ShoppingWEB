using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;
using ShoppingWEB.Views.Login;

namespace ShoppingWEB.Controllers;

public class LoginController : Controller
{
    private readonly SignInManager<UserModel> _signInManager;


    public LoginController(SignInManager<UserModel> signInManager)
    {
        _signInManager = signInManager;
    }


    // GET
    public IActionResult Index()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");

        return View();
    }

    [HttpPost]
    [ActionName("Index")]
    public async Task<IActionResult> OnPostLogin(LoginModel logInfo)
    {
        if (ModelState.IsValid)
        {
            var result = await
                _signInManager.PasswordSignInAsync(logInfo.Username!, logInfo.Password!, logInfo.Remember,
                    false);
            if (!result.Succeeded)
                ModelState.AddModelError(string.Empty, "Sai tên đăng nhập hoặc mật khẩu");
            else
                return RedirectToAction("Index", "Home");
        }
        else
        {
            return View();
        }

        return View();
    }
}