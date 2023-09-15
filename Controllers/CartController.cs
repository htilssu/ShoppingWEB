using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers;

public class CartController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}