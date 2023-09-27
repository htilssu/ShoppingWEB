using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers;

public class ErrorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("Error");
    }
}