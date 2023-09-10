using Microsoft.AspNetCore.Mvc;

namespace Shoppe_Clone.Controllers;

public class RegisterController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}