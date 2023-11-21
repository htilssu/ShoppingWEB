using System.Text;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Controllers.ControllerModels;
using ShoppingWEB.Extension_Method;
using ShoppingWEB.Models;
using X.PagedList;

namespace ShoppingWEB.Controllers;

public class SearchController : Controller
{
    private ShoppingContext _context;

    public SearchController(ShoppingContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? search, int? page, int? orderBy, SearchFilterModel filterModel,
        bool previous = false)
    {
        var productList = await _context.Products.Include(product => product.Seller).ToListAsync();
        if (previous)
        {
            if (filterModel.Location != null)
            {
                productList = productList.Where(p => p.Seller?.Address == filterModel.Location).ToList();
            }

            if (filterModel.IsDiscount is "on")
            {
                productList = productList.Where(p => p.DiscountPercent != 0).ToList();
            }

            if (filterModel.PriceTo != null)
            {
                productList = productList.Where(p => p.Price <= filterModel.PriceTo).ToList();
            }

            if (filterModel.PriceFrom != null)
            {
                productList = productList.Where(p => p.Price >= filterModel.PriceFrom).ToList();
            }

            ViewBag.Location = filterModel.Location ?? "";
            ViewBag.PriceFrom = (filterModel.PriceFrom == null ? "" : filterModel.PriceFrom.ToString())!;
            ViewBag.PriceTo = filterModel.PriceTo == null ? "" : filterModel.PriceTo.ToString()!;
            ViewBag.IsDiscount = filterModel.IsDiscount ?? "";
        }

        ViewBag.Page = page ?? 1;
        ViewBag.Controller = "Search";
        if (string.IsNullOrEmpty(search)) return RedirectToAction("Index", "Home");
        ViewBag.Search = search;
        if (productList.Count == 0)
        {
            productList = await FindProductByName(search);
        }

        //orderBy = 1 (ASC), 0 (DESC), 2 (sold)
        if (orderBy != null)
        {
            productList = orderBy switch
            {
                0 => productList.OrderByDescending(p => p.Price).ToList(),
                1 => productList.OrderBy(p => p.Price).ToList(),
                2 => productList.OrderByDescending(p => p.Sold).ToList(),
                _ => productList
            };
            ViewBag.OrderBy = orderBy;
        }

        ViewBag.IsFindOut = true;
        if (productList.Count != 0) return View(await productList.ToPagedListAsync((int)ViewBag.Page, 16));
        ViewBag.IsFindOut = false;
        var splitName = search.Split();
        foreach (var s in splitName)
        {
            productList.AddRange(await FindProductByName(s));
            if (productList.Count == 0) continue;
            ViewBag.SearchAddition = s;
            break;
        }


        return View(await productList.ToPagedListAsync((int)ViewBag.Page, 16));
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

    public async Task<IActionResult> SearchFilter(string? search, SearchFilterModel filterModel)
    {
        //return if not have search
        if (string.IsNullOrEmpty(search)) return RedirectToAction("Index", "Home");

        var productList = await FindProductByName(search);
        if (filterModel.Location != null)
        {
            productList = productList.Where(p => p.Seller?.Address == filterModel.Location).ToList();
        }

        if (filterModel.IsDiscount is "on")
        {
            productList = productList.Where(p => p.DiscountPercent != 0).ToList();
        }

        if (filterModel.PriceTo != null)
        {
            productList = productList.Where(p => p.Price <= filterModel.PriceTo).ToList();
        }

        if (filterModel.PriceFrom != null)
        {
            productList = productList.Where(p => p.Price >= filterModel.PriceFrom).ToList();
        }

        ViewBag.Location = filterModel.Location ?? "";
        ViewBag.PriceFrom = (filterModel.PriceFrom == null ? "" : filterModel.PriceFrom.ToString())!;
        ViewBag.PriceTo = filterModel.PriceTo == null ? "" : filterModel.PriceTo.ToString()!;
        ViewBag.IsDiscount = filterModel.IsDiscount ?? "";


        ViewBag.IsFindout = productList.Count != 0;
        ViewBag.Search = search;

        return View("Index", await productList.ToPagedListAsync(1, 16));
        
    }
}