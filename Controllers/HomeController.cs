using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using X.PagedList.Mvc;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class HomeController : Controller
{
    private readonly ShoppingContext _context;

    public HomeController(ShoppingContext context)
    {
        _context = context;

    }

    public async Task<IActionResult> Index(int? page)
    {
        int pageSize = 30;
        ViewBag.Controller = "Home";
        int pageNum = (page ?? 1);
        var productList = await _context.Products.Include(p => p.ImageUrls).ToListAsync();
        return View(await productList.ToPagedListAsync(pageNum, pageSize));
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}