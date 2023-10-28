using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Areas.Admin.Models;
using ShoppingWEB.Extension_Method;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoryController : Controller
{
    private readonly ShoppingContext _context;

    public CategoryController(ShoppingContext context)
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
    public async Task<IActionResult> Create(CategoryModel cate)
    {
        var imageFile = cate.ImageFile;
        var category = new Category()
        {
            CategoryName = cate.CategoryName,
        };
        if (ModelState.IsValid)
        {
            if (imageFile is { Length: > 0 })
                try
                {
                    var uploadFolder = Path.Combine("wwwroot", "uploads");
                    var uniqueFileName = Guid.NewGuid() + "_" + imageFile.FileName;
                    var filePath = Path.Combine(uploadFolder, uniqueFileName);
                    category.ImagePath = filePath[(filePath.IndexOf("uploads", StringComparison.Ordinal) - 1)..]
                        .Replace(@"\", "/");
                    await using var stream = new FileStream(filePath, FileMode.Create);
                    await imageFile.CopyToAsync(stream);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return View();
                }

            _context.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(cate);
    }

    // GET: Category/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var category = await _context.Categories.FindAsync(id);
        if (category == null) return NotFound();
        return View(category);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, CategoryModel category)
    {
        var oldProduct = await _context.Categories.FindAsync(id);
        if (ModelState.IsValid)
        {
            if (category.ImageFile?.FileName is not (null or ""))
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
                    oldProduct.ImagePath = filePath[(filePath.IndexOf("uploads", StringComparison.Ordinal) - 1)..]
                        .Replace(@"\", "/");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                //Delete old image
                oldPath.DeleteFile();


                oldProduct.CategoryName = category.CategoryName;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    // ignored
                }

                return RedirectToAction(nameof(Index));
            }
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

        category?.ImagePath?.DeleteFile();

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}