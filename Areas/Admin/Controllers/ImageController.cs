using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Areas.Admin.Models;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers;

[Area("Admin")]
public class ImageController : Controller
{
    private readonly ApplicationDbContext _context;

    public ImageController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Image

    public async Task<IActionResult> Index(string product)
    {
        if (string.IsNullOrEmpty(product)) return RedirectToAction("Index", "Product");

        var productImageQ = _context.ImageUrl.Where(url => url.ProductId == product);
        var productImageList = await productImageQ.ToListAsync();
        ViewBag.ProductId = product;

        return View(productImageList);
    }

    // GET: Admin/Image/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || _context.ImageUrl == null) return NotFound();

        var imageUrl = await _context.ImageUrl
            .FirstOrDefaultAsync(m => m.ImagePath == id);
        if (imageUrl == null) return NotFound();

        return View(imageUrl);
    }

    // GET: Admin/Image/Create
    public IActionResult Create(string product)
    {
        ViewBag.ProductId = product;
        return View();
    }

    // POST: Admin/Image/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ImageModel imageUrl)
    {
        var imageFile = imageUrl.ImageFile;
        if (!string.IsNullOrEmpty(imageFile.FileName))
        {
            var uploadFolder = Path.Combine("wwwroot", "uploads");
            var fileName = Path.Combine(Guid.NewGuid() + "_" + imageFile.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);
            imageUrl.ImagePath = filePath;
            try
            {
                using (Stream stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return View();
            }
        }

        if (ModelState.IsValid)
        {
            _context.Add(new ImageUrl
            {
                ImagePath = imageUrl.ImagePath!,
                ProductId = imageUrl.ProductId,
                Thumnail = imageUrl.Thumnail
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return Redirect($"/Admin/Image/Create?id={imageUrl.ProductId}");
    }

    // GET: Admin/Image/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.ImageUrl == null) return NotFound();

        var imageUrl = await _context.ImageUrl.FindAsync(id);
        if (imageUrl == null) return NotFound();
        return View(imageUrl);
    }

    // POST: Admin/Image/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("ImagePath,ProductId,Thumnail")] ImageUrl imageUrl)
    {
        if (id != imageUrl.ImagePath) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(imageUrl);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageUrlExists(imageUrl.ImagePath))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(imageUrl);
    }

    // GET: Admin/Image/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.ImageUrl == null) return NotFound();

        var imageUrl = await _context.ImageUrl
            .FirstOrDefaultAsync(m => m.ImagePath == id);
        if (imageUrl == null) return NotFound();

        return View(imageUrl);
    }

    // POST: Admin/Image/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.ImageUrl == null) return Problem("Entity set 'ApplicationDbContext.ImageUrl'  is null.");
        var imageUrl = await _context.ImageUrl.FindAsync(id);
        if (imageUrl != null) _context.ImageUrl.Remove(imageUrl);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ImageUrlExists(string id)
    {
        return (_context.ImageUrl?.Any(e => e.ImagePath == id)).GetValueOrDefault();
    }
}