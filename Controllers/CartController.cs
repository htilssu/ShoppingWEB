using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class CartController : Controller
{
    private ShoppingContext _context;
    private UserManager<UserModel> _userManager;
    private SignInManager<UserModel> _signInManager;

    public CartController(ShoppingContext context, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var cartUser = _context.Carts.FirstOrDefault(c => c.CustomerId == user!.Id);
        
        
        return View(cartUser);
    }

    public async Task<IActionResult> AddToCard(CartItem)
    {
        return View("Index");
    }
}