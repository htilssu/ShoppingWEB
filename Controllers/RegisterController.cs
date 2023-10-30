using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;
using ShoppingWEB.Views.Register;

namespace ShoppingWEB.Controllers;

public class RegisterController : Controller
{
    private readonly SignInManager<UserModel> _signInManager;
    private readonly UserManager<UserModel> _userManager;

    public RegisterController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }


    // GET
    public IActionResult Index()
    {
        if (User.Identity!.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    [ActionName("Index")]
    public async Task<IActionResult> OnPostRegister(RegistModel registModel)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByNameAsync(registModel.UserName!);
            if (user == null)
            {
                var result = await _userManager.CreateAsync(new UserModel
                {
                    UserName = registModel.UserName,
                    Email = registModel.Email,
                    PhoneNumber = registModel.NumberPhone
                }, registModel.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(registModel.UserName!, registModel.Password!, false, false);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.TryAddModelError("exist", "Người dùng đã tồn tại");
                /*TODO error didn't display in View*/
                return View();
            }
        }

        return View();
    }
}