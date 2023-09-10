using Microsoft.AspNetCore.Mvc;

namespace Shoppe_Clone.Controllers;

public class ErrorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("Error");
    }
}