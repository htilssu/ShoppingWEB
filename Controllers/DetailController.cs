using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers;

public class DetailController : Controller
{
    private readonly ApplicationDbContext _db;

    public DetailController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET
    public async Task<IActionResult> Index(string id)
    {
        var product = await _db.Products.FindAsync(id);
        // if (product is null) return RedirectToAction("Index", "Home");
        return View();
    }
}