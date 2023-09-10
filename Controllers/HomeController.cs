using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace ShoppingWEB.Controllers;

public class HomeController : Controller
{

    public IActionResult Index()
    {
        return View();
    }
}