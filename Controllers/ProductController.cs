using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class ProductController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Product
    public async Task<IActionResult> Index()
    {
        return _context.Products != null
            ? View(await _context.Products.ToListAsync())
            : Problem("Entity set 'ApplicationDbContext.Products'  is null.");
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || _context.Products == null) return NotFound();

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }

    // GET: Product/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Product/Create
    [HttpPost]
    [ActionName("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        product.Id = Guid.NewGuid().ToString();
        _context.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));


        return View(product);
    }

    // GET: Product/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.Products == null) return NotFound();

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    // POST: Product/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id,
        [Bind(
            "ProductName,Price,DiscountPrice,InStock,ShortDescription,ProductDescription,Published,CategoryId")]
        Product product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(product);
    }

    // GET: Product/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.Products == null) return NotFound();

        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);
        if (product == null) return NotFound();

        return View(product);
    }

    // POST: Product/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.Products == null) return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
        var product = await _context.Products.FindAsync(id);
        if (product != null) _context.Products.Remove(product);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ProductExists(string id)
    {
        return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
    }
}