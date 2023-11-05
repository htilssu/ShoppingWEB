using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;
using ShoppingWEB.Views.Register;
using ZstdSharp.Unsafe;

namespace ShoppingWEB.Controllers;

public class RegisterController : Controller
{
    private readonly SignInManager<UserModel> _signInManager;
    private readonly UserManager<UserModel> _userManager;
    private ShoppingContext _context;

    public RegisterController(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager,
        ShoppingContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
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
                var userId = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(new UserModel
                {
                    Id = userId,
                    UserName = registModel.UserName,
                    Email = registModel.Email,
                    PhoneNumber = registModel.NumberPhone
                }, registModel.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.PasswordSignInAsync(registModel.UserName!, registModel.Password!, false,
                        false);
                    _context.Carts.Add(new Cart()
                    {
                        Id = Guid.NewGuid().ToString(),
                        CustomerId = userId
                    });
                    await _context.SaveChangesAsync();
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