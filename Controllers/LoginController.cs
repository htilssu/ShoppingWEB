using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shoppe_Clone.Models;

namespace ShoppingWEB.Controllers;

public class LoginController : Controller
{
    // GET
    private readonly SignInManager<UserModel> _signInManager;

    public LoginController(SignInManager<UserModel> signInManager)
    {
        _signInManager = signInManager;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [ActionName("Index")]
    public async Task<IActionResult> LoginAction(LoginModel usr)
    {
   
        return RedirectToAction("Index", "Home");
    }
}