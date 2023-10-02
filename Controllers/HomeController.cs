using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var productList = _context.Products.ToList();
        return productList != null ? View(productList) : Problem("");
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}