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

                var userEmail = _context.Users.Any(u => u.Email == registModel.Email);
                if (userEmail)
                {
                    ViewBag.EmailExist = "Email đã được đăng ký";
                    return View();
                }

                if (_context.Users.Any(u => u.PhoneNumber == registModel.NumberPhone))
                {
                    ViewBag.NumberExist = "Số điện thoại đã được đăng ký";
                    return View();
                }
                
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
                    _context.Carts.Add(new Cart
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
                ViewBag.UserExist = "Người dùng đã tồn tại";
                return View();
            }
        }

        return View();
    }
}