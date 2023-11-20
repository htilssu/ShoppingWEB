using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Controllers.ControllerModels;
using ShoppingWEB.Models;
using X.PagedList;

namespace ShoppingWEB.Controllers;

public class CategoryController : Controller
{
    private readonly ShoppingContext _context;

    public CategoryController(ShoppingContext context)
    {
        _context = context;
    }

    // GET: Category
    public async Task<IActionResult> Index(string? id, int? page, int? orderBy, SearchFilterModel filterModel,
        bool previous = false)
    {
        ViewBag.Controller = "Category";
        ViewBag.CategoryId = id;


        if (string.IsNullOrEmpty(id))
        {
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Id = id;

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

        var pageNum = page ?? 1;
        productList = productList
            .Where(p => p.CategoryId == id).ToList();
        var result = await productList.ToPagedListAsync(pageNum, 16);
        ViewBag.Controller = "Category";
        ViewBag.CategoryId = id;
        return View(result);
    }


    public async Task<IActionResult> SearchFilter(string? id, SearchFilterModel filterModel)
    {
        //return if not have search
        if (string.IsNullOrEmpty(id)) return RedirectToAction("Index", "Home");

        var productList = await _context.Products.Where(p => p.CategoryId == id).ToListAsync();
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
        ViewBag.Controller = "Category";


        ViewBag.IsFindout = productList.Count != 0;
        ViewBag.Id = id;
        // return View("Index");
        return View("~/Views/Category/Index.cshtml", await productList.ToPagedListAsync(1, 16));
    }
}