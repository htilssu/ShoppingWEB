using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class BillController : Controller
{
    private ShoppingContext _context;

    public BillController(ShoppingContext context)
    {
        _context = context;
    }

    // GET
    public IActionResult Index(string? id)
    {
        var bill = _context.Bills.FirstOrDefault(b => b.Id == id);
        return View(bill);
    }
}