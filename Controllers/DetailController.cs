using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class DetailController : Controller
{
    private readonly ShoppingContext _db;
    private UserManager<UserModel> _manager;

    public DetailController(ShoppingContext db, UserManager<UserModel> manager)
    {
        _db = db;
        _manager = manager;
    }

    // GET
    public async Task<IActionResult> Index(string? id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return RedirectToAction("Index", "Home");
        }

        var product = _db.Products
            .Include(p => p.TypeProducts)
            .ThenInclude(t => t.Sizes)
            .Include(p => p.ImageUrls)
            .Include(p => p.Coupons)
            .FirstOrDefault(p => p.Id == id)!;
        return View(product);
    }

    public async Task<IActionResult> OnBuying(BuyingModel product)
    {
        var user = await _manager.GetUserAsync(User);
        if (user != null)
        {
        }

        return View("Index");
    }
}

public class BuyingModel : Product
{
    public bool IsBuying { get; set; }
}