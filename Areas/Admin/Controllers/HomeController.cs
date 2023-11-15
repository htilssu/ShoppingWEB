using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    // GET
    public IActionResult Index()
    {
        ViewBag.Title = "Trang Admin";
        return View();
    }
}