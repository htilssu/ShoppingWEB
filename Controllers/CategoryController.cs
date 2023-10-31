using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB;
using ShoppingWEB.Models;

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
        public async Task<IActionResult> Index(string? id)
        {
            return View(await _context.Categories.ToListAsync());
        }
    }
}