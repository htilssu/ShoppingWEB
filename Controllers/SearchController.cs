using Microsoft.AspNetCore.Mvc;

namespace Shoppe_Clone.Controllers;

public class SearchController : Controller
{
    public IActionResult Index(string search)
    {
        return View(model: search);
    }
}