using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

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

        if (ModelState.IsValid)
        {
            return RedirectToAction("Index","Home");
        }

        return View(usr);
    }
}