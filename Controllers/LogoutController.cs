using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class LogoutController : Controller
{
    private readonly SignInManager<UserModel> _signInManager;

    public LogoutController(SignInManager<UserModel> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}