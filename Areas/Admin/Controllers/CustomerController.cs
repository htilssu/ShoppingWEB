using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Controllers;
[Area("Admin")]
public class CustomerController : Controller
{
    private readonly ShoppingContext _context;

    public CustomerController(ShoppingContext context)
    {
        _context = context;
    }

    // GET: Admin/Customer
    public async Task<IActionResult> Index()
    {
        var listUser = await _context.Users.ToListAsync();
        return View(listUser);
    }
    
    //Viết hàm xóa khách hàng 
    public async Task<IActionResult> RemoveUser(string? id) //truyền vào 1 id
    {
        var user = _context.Users.FirstOrDefault(i => i.Id == id); //lấy id
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}