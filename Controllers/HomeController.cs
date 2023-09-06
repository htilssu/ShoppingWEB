using Microsoft.AspNetCore.Mvc;

namespace Shoppe_Clone.Controllers;

public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}