using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Areas.Admin.Models;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private ShoppingContext _context;

    public HomeController(ShoppingContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var analysis = new AnalysisModel
        {
            EarningMonthly = _context.Bills
                .Where(b => b.Date != null && b.Date.Value.Month == DateTime.Now.Month)
                .Sum(b => b.Total),
            EarningYearly = _context.Bills
                .Where(b => b.Date != null && b.Date.Value.Year == DateTime.Now.Year)
                .Sum(b => b.Total)
        };
        return View(analysis);
    }
}