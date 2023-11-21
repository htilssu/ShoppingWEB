using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Extension_Method;
using ShoppingWEB.Models;
using X.PagedList;

namespace ShoppingWEB.Controllers;

[Authorize]
public class ProfileController : Controller
{
    private readonly UserManager<UserModel> _userManager;
    private ShoppingContext _context;

    public ProfileController(UserManager<UserModel> userManager, ShoppingContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        return View(user);
    }

    public async Task<IActionResult> Bill(int? page)
    {
        var pageSize = 15;
        ViewBag.Controller = "Home";
        var pageNum = (page ?? 1);
        var user = await _userManager.GetUserAsync(User);
        var listBillItem = _context.BillItems.ToList();
        var billList = _context.Bills.ToList();
        var userBillItem = (from bill in billList
            join billItem in listBillItem on bill.Id equals billItem.BillId
            where bill.UserId == user.Id
            select billItem).ToList();

        return View(await userBillItem.ToPagedListAsync(pageNum, pageSize));
    }

    public async Task<IActionResult> OnPostEditUser(UserModel userModel)
    {
        var user = await _userManager.GetUserAsync(User);

        //Check email and phone number
        if (!Valid.IsValidEmail(userModel.Email) && !Valid.IsValidPhoneNumber(userModel.PhoneNumber))
        {
            return NotFound();
        }

        if (user != null && user.UserName != userModel.UserName)
        {
            return NotFound();
        }

        user!.Email = userModel.Email;
        user.PhoneNumber = userModel.PhoneNumber;
        user.Gender = userModel.Gender;
        user.BirthDay = userModel.BirthDay;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Profile");
    }

    public async Task<IActionResult> OnPostEditAvatar(IFormFile? Avatar)
    {
        if (Avatar == null)
        {
            return NotFound();
        }

        var user = await _userManager.GetUserAsync(User);

        if (user == null)
        {
            return NotFound();
        }

        var imagePath = await Avatar.SaveImageAsync();
        if (!user.AvtPath.Contains("avatars/"))
        {
            user.AvtPath.DeleteFile();
        }

        user.AvtPath = imagePath;
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", "Profile");
    }
}