using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class HomeController : Controller
{
    private readonly ShoppingContext _context;

    public HomeController(ShoppingContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var productList = _context.Products.ToList();
        return View(productList);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}