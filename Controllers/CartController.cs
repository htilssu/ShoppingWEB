using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class CartController : Controller
{
    private ShoppingContext _context;

    public CartController(ShoppingContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> AddToCard(Product product)
    {
        return View("Index");
    }
}