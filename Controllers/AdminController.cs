using Microsoft.AspNetCore.Mvc;

namespace Shoppe_Clone.Controllers;

public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}