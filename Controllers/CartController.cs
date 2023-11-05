using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
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
        var cartUser = _context.Carts
            .Include(p => p.CartItems)
            .ThenInclude(i => i.TypeProduct)
            .ThenInclude(i => i!.Product)
            .FirstOrDefault(c => c.CustomerId == user!.Id);
        
        return View(cartUser);
    }

    //Action thêm product vào giỏ hàng
    public async Task<IActionResult> AddToCard(CartItem cartItem)
    {
        if (ModelState.IsValid)  //kiem tra cartItem co null khong
        {
            _context.CartItems.Add(cartItem);
           await _context.SaveChangesAsync();
        }
        
        return View("Index");
    }

    //xóa dòng sản phẩm trong Giỏ hàng
    /*public async Task<IActionResult> RemoveCart(int id)
    {
    }*/
}