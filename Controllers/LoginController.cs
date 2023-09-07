using Microsoft.AspNetCore.Mvc;

namespace Shoppe_Clone.Controllers;

public class LoginController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}