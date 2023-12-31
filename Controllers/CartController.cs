﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

[Authorize] //bat buoc phai dang nhap
public class CartController : Controller
{
    private ShoppingContext _context;
    private UserManager<UserModel> _userManager;

    public CartController(ShoppingContext context, UserManager<UserModel> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
        var user = await _userManager.GetUserAsync(User);
        var cartUser = _context.Carts
            .FirstOrDefault(c => c.CustomerId == user!.Id);

        return View(cartUser);
    }

    //Action thêm product vào giỏ hàng
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCard(CartItem cartItem)
    {
        if (ModelState.IsValid) //kiem tra cartItem co null khong
        {
            var user = await _userManager.GetUserAsync(User);
            var cartUser = _context.Carts.Include(c => c.CartItems)
                .FirstOrDefault(c => c.CustomerId == user!.Id);
            bool isExist = false;
            foreach (var cartUserCartItem in cartUser.CartItems)
            {
                if (cartUserCartItem.TypeProductId == cartItem.TypeProductId)
                {
                    if (cartUserCartItem.SizeType == cartItem.SizeType)
                    {
                        cartUserCartItem.Quantity += cartItem.Quantity < 0 ? 0 : cartItem.Quantity;
                        break;
                    }
                    else
                    {
                        cartItem.CartId = cartUser.Id;
                        _context.CartItems.Add(cartItem);
                        break;
                    }
                    
                }
            }

            if (!isExist)
            {
                var remainingQuantity = _context.TypeProducts.FirstOrDefault(t => t.Id == cartItem.TypeProductId)?.Sizes
                    .FirstOrDefault(s => s.SizeType == cartItem.SizeType)?.Quantity;
                if (cartItem.Quantity <= remainingQuantity)
                {
                    cartItem.CartId = cartUser.Id;
                    _context.CartItems.Add(cartItem);
                }
              
            }
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    //viết hàm lấy tổng số sản phẩm có trong Giỏ hàng, sau đó Render đến PartialView
    public PartialViewResult BagCart()
    {
        var count = 0;
        int soLuongSanPham = 0;
        // Giả sử _context là DbContext chứa DbSet Carts
        var danhSachGioHang = _context.Carts.ToList();
        foreach (var product in danhSachGioHang)
        {
            if (product != null)
            {
                count++;
            }
        }
        ViewBag.SoLuongSanPham = count;
        return PartialView("BagCart");
    }

    //Viết hàm xóa sản phẩm trong giỏ hàng
    public async Task<IActionResult> RemoveCart(string? id) //truyền vào 1 id
    {
        var cartItem = _context.CartItems.FirstOrDefault(i => i.Id == id); //lấy id
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }

//Xóa tất cả sản phẩm trong Cart
    public async Task<IActionResult> ClearCart(string? cartId)
    {
        // Lấy tất cả sản phẩm trong giỏ hàng
        var cartItems = _context.CartItems.Where(c => c.CartId == cartId).ToList();
        foreach (var cartItem in cartItems)
        {
            _context.CartItems.Remove(cartItem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> ChangTypeProduct(string? cartItemId, string? typeId)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);
        if (cartItem != null)
        {
            cartItem.TypeProductId = typeId;
        }

        return Ok();
    }

    public async Task<IActionResult> UpdateQuantity(string? cartItemId, int? quantity)
    {
        if (cartItemId == null)
        {
            return NotFound();
        }

        var cartItem = await _context.CartItems.FirstOrDefaultAsync(c => c.Id == cartItemId);
        if (quantity == null || string.IsNullOrEmpty(quantity.ToString()))
        {
            return NotFound();
        }

        if (cartItem != null)
        {
            cartItem.Quantity = quantity < 0 ? 0 : quantity;
        }

        await _context.SaveChangesAsync();
        return cartItem != null ? Ok() : NotFound();
    }
}