using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}