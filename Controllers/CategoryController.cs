using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;
using X.PagedList;

namespace ShoppingWEB.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ShoppingContext _context;

        public CategoryController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: Category
        public async Task<IActionResult> Index(string? id, int? page)
        {
            var pageNum = page ?? 1;
            var productList = await _context.Products
                .Include(p => p.ImageUrls)
                .Where(p => p.CategoryId == id).ToListAsync();
            var result = await productList.ToPagedListAsync(pageNum, 16);
            ViewBag.Controller = "Category";
            ViewBag.CategoryId = id;
            return View(result);
        }
    }
}