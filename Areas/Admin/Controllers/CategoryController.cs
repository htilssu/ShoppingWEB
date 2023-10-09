using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Areas.Admin.Models;
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
        return View(await _context.Categories.OrderBy(category => category.CategoryName).ToListAsync());
    }

    // GET: Category/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();

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
    public async Task<IActionResult> Create(Category category, IFormFile? imageFile)
    {
        if (ModelState.IsValid)
        {
            if (imageFile is { Length: > 0 })
                try
                {
                    var uploadFolder = Path.Combine("wwwroot", "uploads");
                    var uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);
                    category.ImagePath = filePath;
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(stream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View();
                }

            category.UpdatedAt = DateTime.Now;
            category.CreatedAt = DateTime.Now;
            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(category);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }

    // POST: Category/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, CategoryModel category)
    {
        var oldProduct = await _context.Categories.FindAsync(id);
        if (ModelState.IsValid)
        {
            if (category.ImageFile.FileName is not (null or ""))
            {
                var oldPath = oldProduct!.ImagePath!;
                var file = category.ImageFile;
                var rootPath = Path.Combine("wwwroot", "uploads");
                var fileName = Guid.NewGuid() + "_" + file.FileName;
                var filePath = Path.Combine(rootPath, fileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                try
                {
                    await file.CopyToAsync(stream);
                    oldProduct.ImagePath = filePath;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                //Delete old image
                try
                {
                    System.IO.File.Delete(oldPath);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }


            oldProduct!.UpdatedAt = DateTime.Now;
            oldProduct.CategoryName = category.CategoryName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(oldProduct);
    }

    // GET: Category/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();

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
        var category = await _context.Categories.FindAsync(id);
        _context.Categories.Remove(category!);
        
        try
        {
            System.IO.File.Delete(category!.ImagePath!);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CategoryExists(string id)
    {
        return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}