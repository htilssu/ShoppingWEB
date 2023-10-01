using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class LogoutController : Controller
{
    private readonly SignInManager<UserModel> _signInManager;
    private UserManager<UserModel> _userManager;

    public LogoutController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    public async Task<IActionResult> Index()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}