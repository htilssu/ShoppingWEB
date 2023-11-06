using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities.IO.Pem;
using ShoppingWEB.Extension_Method;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class SearchController : Controller
{
    private ShoppingContext _context;

    public SearchController(ShoppingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search,int?page)
    {
        ViewBag.Page = page ?? 1;
        if (string.IsNullOrEmpty(search)) return RedirectToAction("Index", "Home");
        ViewBag.Search = search;
        var listProduct = await FindProductByName(search);
        ViewBag.IsFindOut = true;
        if (listProduct.Count != 0) return View(listProduct);
        ViewBag.IsFindOut = false;
        var splitName = search.Split();
        foreach (var s in splitName)
        {
            listProduct.AddRange(await FindProductByName(s));
            if (listProduct.Count == 0) continue;
            ViewBag.SearchAddition = s;
            break;
        }


        return View(listProduct);
    }

    private async Task<List<Product>> FindProductByName(string name)
    {
        var result1 = await _context
            .Products
            .Include(p => p.ImageUrls)
            .Where(product => product.ProductName!.Contains(name)).ToListAsync();
        var listResult = new List<Product>();
        listResult.AddRange(result1);
        await foreach (var contextProduct in _context.Products)
            if (contextProduct.ProductName != null &&
                contextProduct.ProductName.RemoveSpecialMark().Contains(name.RemoveSpecialMark()))
                listResult.Add(contextProduct);
        return listResult;
    }
}