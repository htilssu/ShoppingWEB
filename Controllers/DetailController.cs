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
    public IActionResult Index(string? id, int? page)
    {
        ViewBag.Page = page ?? 1;
        ViewBag.Controller = "Detail";
        ViewBag.ProductId = id ?? "";

        if (string.IsNullOrEmpty(id))
        {
            return RedirectToAction("Index", "Home");
        }

        var product = _db.Products.FirstOrDefault(p => p.Id == id)!;
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