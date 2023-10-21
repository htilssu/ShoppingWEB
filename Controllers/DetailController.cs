using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> Index(string id)
    {
        var product = await _db.Products.FindAsync(id);
        // if (product is null) return RedirectToAction("Index", "Home");
        return View();
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
    public Category Category { get; set; }
}