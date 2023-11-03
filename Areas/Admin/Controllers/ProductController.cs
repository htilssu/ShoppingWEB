using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Areas.Admin.Models;
using ShoppingWEB.Extension_Method;
using ShoppingWEB.Models;


namespace ShoppingWEB.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ShoppingContext _context;

        public ProductController(ShoppingContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                var categoryProduct =
                    await _context.Products
                        .Where(product => product.ProductName == s || product.ProductName!.Contains(s))
                        .ToListAsync();
                return View(categoryProduct);
            }

            var productList = await _context.Products.ToListAsync();
            return View(productList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostCreate(ProductModel product)
        {
            if (!ModelState.IsValid) return RedirectToAction("Index", "Product");
            try
            {
                //Binding ProductTypeId for size
                foreach (var productTypeProduct in product.TypeProducts)
                {
                    var typeImagePath = await productTypeProduct.ImageFile.SaveImage();
                    productTypeProduct.ImagePath = typeImagePath;
                    foreach (var size in productTypeProduct.Sizes)
                    {
                        size.TypeProductId = productTypeProduct.Id;
                    }
                }

                foreach (var formFile in product.ImageFile)
                {
                    var pathImage = await formFile.SaveImage();
                    product.ImageUrls.Add(new ImageUrl()
                    {
                        Product = product,
                        ImagePath = pathImage,
                        Thumbnail = false,
                        ProductId = product.Id,
                    });
                }

                var thumbnailPath = await product.Thumbnail.SaveImage();
                product.ImageUrls.Add(new ImageUrl()
                {
                    Product = product,
                    ImagePath = thumbnailPath,
                    Thumbnail = true,
                    ProductId = product.Id,
                });

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            return View();
        }

        public async Task<IActionResult> OnEditPost(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FindAsync(productModel.Id);
                if (product != null)
                {
                    var field = typeof(Product).GetFields();
                    foreach (var fieldInfo in field)
                    {
                        var value = fieldInfo.GetValue(productModel);
                        fieldInfo.SetValue(product, value);
                    }
                }
            }

            return RedirectToAction("Index", "Product");
        }

        public IActionResult Detail(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            return View();
        }


        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPostDelete(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            var targetProduct = await _context.Products.FindAsync(id);
            if (targetProduct != null)
            {
                foreach (var targetProductImageUrl in targetProduct.ImageUrls)
                {
                    targetProductImageUrl.ImagePath.DeleteFile();
                }

                foreach (var targetProductTypeProduct in targetProduct.TypeProducts)
                {
                    targetProductTypeProduct.ImagePath!.DeleteFile();
                }
            }


            if (targetProduct != null) _context.Products.Remove(targetProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Product");
        }
    }
}