using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class HomeController : Controller
{
    private readonly ShoppingContext _context;

    public HomeController(ShoppingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var productList = await _context.Products.Include(p => p.ImageUrls).ToListAsync();
        return View(productList);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}