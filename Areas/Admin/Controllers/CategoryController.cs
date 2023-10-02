using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public CategoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index()
    {
        return _context.Categories != null
            ? View(await _context.Categories.ToListAsync())
            : Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
    }

    // GET: Category/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || _context.Categories == null) return NotFound();

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (category == null) return NotFound();

        return View(category);
    }

    // GET: Category/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Category category)
    {
        if (ModelState.IsValid)
        {
            category.UpdatedAt = DateTime.Now;
            category.CreatedAt = DateTime.Now;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.Categories == null) return NotFound();

        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    // POST: Category/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id,
        [Bind("Id,CategoryName,ImagePath,CreatedAt,UpdatedAt,UpdatedBy")]
        Category category)
    {
        if (id != category.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(category.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Categories == null) return NotFound();

        var category = await _context.Categories
            .FirstOrDefaultAsync(m => m.Id == id);
        if (category == null) return NotFound();

        return View(category);
    }

    // POST: Category/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.Categories == null) return Problem("Entity set 'ApplicationDbContext.Categories'  is null.");
        var category = await _context.Categories.FindAsync(id);
        if (category != null) _context.Categories.Remove(category);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(string id)
    {
        return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}