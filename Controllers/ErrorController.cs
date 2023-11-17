using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers;

public class ErrorController : Controller
{
    public IActionResult Index()
    {
        return View("Error");
    }
}