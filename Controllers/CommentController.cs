using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Controllers;

public class CommentController : Controller
{
    
    private ShoppingContext _context;

    public CommentController(ShoppingContext context)
    {
        _context=context;
    }
    // GET
    public IActionResult Index()
    {
        return View();
    }
}