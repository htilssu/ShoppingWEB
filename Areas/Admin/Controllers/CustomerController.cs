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
        var roleAdmin = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        if (roleAdmin != null)
        {
           var listAdmin =await _context.UserRoles.Where(u => u.RoleId == roleAdmin.Id).ToListAsync();
           var listUser = await _context.Users.ToListAsync();
           foreach (var userRole in listAdmin)
           {
               listUser = listUser.Where(u => u.Id != userRole.UserId).ToList();  //lay ra list != admin
           }
           return View(listUser);

        }
        return View("Index");
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