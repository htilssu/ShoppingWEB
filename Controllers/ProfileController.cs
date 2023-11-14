﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoppingWEB.Models;

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
        var listBillItem = _context.BillItems.ToList();
        var billList = _context.Bills.ToList();
        var userBillItem = (from bill in billList
            join billItem in listBillItem on bill.Id equals billItem.BillId
            where bill.UserId == user.Id
            select billItem).ToList();
        
        return View(userBillItem);
    }
}