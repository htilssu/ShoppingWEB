using Microsoft.AspNetCore.Mvc;

 namespace ShoppingWEB.Controllers;
public class AdminController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}