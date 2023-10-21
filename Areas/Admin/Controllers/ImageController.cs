using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Areas.Admin.Models;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers;

[Area("Admin")]
public class ImageController : Controller
{
    private readonly ShoppingContext _context;

    public ImageController(ShoppingContext context)
    {
        _context = context;
    }

    // GET: Admin/Image

    public async Task<IActionResult> Index(string product)
    {
        if (string.IsNullOrEmpty(product)) return RedirectToAction("Index", "Product");

        var productImageQ = _context.ImageUrls.Where(url => url.ProductId == product);
        var productImageList = await productImageQ.ToListAsync();
        ViewBag.ProductId = product;

        return View(productImageList);
    }

    // GET: Admin/Image/Details/5
    public async Task<IActionResult> Details(string id)
    {
        if (id == null || _context.ImageUrls == null) return NotFound();

        var ImageUrls = await _context.ImageUrls
            .FirstOrDefaultAsync(m => m.ImagePath == id);
        if (ImageUrls == null) return NotFound();

        return View(ImageUrls);
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
    public async Task<IActionResult> Create(ImageModel ImageUrls)
    {
        var imageFile = ImageUrls.ImageFile;
        if (!string.IsNullOrEmpty(imageFile.FileName))
        {
            var uploadFolder = Path.Combine("wwwroot", "uploads");
            var fileName = Path.Combine(Guid.NewGuid() + "_" + imageFile.FileName);
            var filePath = Path.Combine(uploadFolder, fileName);
            ImageUrls.ImagePath = filePath;
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
            _context.Add(new ImageUrl()
            {
                ImagePath = ImageUrls.ImagePath!,
                ProductId = ImageUrls.ProductId,
                Thumnail = ImageUrls.Thumnail
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return Redirect($"/Admin/Image/Create?id={ImageUrls.ProductId}");
    }

    // GET: Admin/Image/Edit/5
    public async Task<IActionResult> Edit(string id)
    {
        if (id == null || _context.ImageUrls == null) return NotFound();

        var ImageUrls = await _context.ImageUrls.FindAsync(id);
        if (ImageUrls == null) return NotFound();
        return View(ImageUrls);
    }

    // POST: Admin/Image/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, [Bind("ImagePath,ProductId,Thumnail")] ImageUrl ImageUrls)
    {
        if (id != ImageUrls.ImagePath) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(ImageUrls);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImageUrlsExists(ImageUrls.ImagePath))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(ImageUrls);
    }

    // GET: Admin/Image/Delete/5
    public async Task<IActionResult> Delete(string id)
    {
        if (id == null || _context.ImageUrls == null) return NotFound();

        var ImageUrls = await _context.ImageUrls
            .FirstOrDefaultAsync(m => m.ImagePath == id);
        if (ImageUrls == null) return NotFound();

        return View(ImageUrls);
    }

    // POST: Admin/Image/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        if (_context.ImageUrls == null) return Problem("Entity set 'ApplicationDbContext.ImageUrls'  is null.");
        var ImageUrls = await _context.ImageUrls.FindAsync(id);
        if (ImageUrls != null) _context.ImageUrls.Remove(ImageUrls);

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ImageUrlsExists(string id)
    {
        return (_context.ImageUrls?.Any(e => e.ImagePath == id)).GetValueOrDefault();
    }
}