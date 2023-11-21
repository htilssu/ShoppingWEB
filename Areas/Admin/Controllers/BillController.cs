using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BillController : Controller
    {
        private readonly ShoppingContext _context;

        public BillController(ShoppingContext context)
        {
            _context = context;
        }

        // GET: Admin/Bill
        public async Task<IActionResult> Index()
        {
            var shoppingContext = await _context.Bills.ToListAsync();
            return View(shoppingContext);
        }

        // GET: Admin/Bill/Details/5
        public async Task<IActionResult> Detail(string? id)
        {
            // if (id == null || _context.Bills == null)
            // {
            //     return NotFound();
            // }

            var bill = await _context.Bills
                .Include(b => b.Delivery)
                .Include(b => b.PaymentMethodNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            // if (bill == null)
            // {
            //     return NotFound();
            // }

            return View(bill);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("Id,TypeProductId,Total,PaymentMethod,Quantity,DeliveryId")]
            Bill bill)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bill);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DeliveryId"] = new SelectList(_context.DeliveryTypes, "Id", "Id", bill.DeliveryId);
            ViewData["PaymentMethod"] = new SelectList(_context.PaymentMethods, "Id", "Id", bill.PaymentMethod);
            // TODO return
            return View("Index");
        }
    }
}