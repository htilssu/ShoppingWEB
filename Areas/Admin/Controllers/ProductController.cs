using System.Collections;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        public async Task<IActionResult> Index(string? s, string? categoryId)
        {
            if (!string.IsNullOrEmpty(categoryId))
            {
                ViewBag.CategoryId = categoryId;
                var products = await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
                if (!string.IsNullOrEmpty(s))
                {
                    var resultProduct = products
                        .Where(p => p.ProductName!.ToUpper().Contains(s.ToUpper()) ||
                                    string.Equals(p.ProductName, s, StringComparison.CurrentCultureIgnoreCase))
                        .ToList();

                    return View(resultProduct);
                }

                return View(products);
            }

            if (!string.IsNullOrEmpty(s))
            {
                var productQ = await _context.Products.Where(p => p.ProductName!.Contains(s)).ToListAsync();
                return View(productQ);
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
                    var typeImagePath = await productTypeProduct.ImageFile!.SaveImageAsync();
                    productTypeProduct.ImagePath = typeImagePath;
                    foreach (var size in productTypeProduct.Sizes)
                    {
                        size.TypeProductId = productTypeProduct.Id;
                    }
                }

                foreach (var formFile in product.ImageFile!)
                {
                    var pathImage = await formFile.SaveImageAsync();
                    product.ImageUrls.Add(new ImageUrl()
                    {
                        Product = product,
                        ImagePath = pathImage,
                        Thumbnail = 0,
                        ProductId = product.Id,
                    });
                }

                var thumbnailPath = await product.Thumbnail!.SaveImageAsync();
                product.ImageUrls.Add(new ImageUrl()
                {
                    Product = product,
                    ImagePath = thumbnailPath,
                    Thumbnail = 1,
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

        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product != null)
            {
                return View(product);
            }

            return RedirectToAction("Index", "Product");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public async Task<IActionResult> OnEditPost(ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productModel.Id);

                if (product != null)
                {
                    if (productModel.Thumbnail != null)
                    {
                        var thumbnailPath = await productModel.Thumbnail.SaveImageAsync();
                        var thumbnailImage = await _context.ImageUrls
                            .FirstOrDefaultAsync(i => i.ProductId == product.Id && i.Thumbnail == 1);
                        thumbnailImage!.ImagePath.DeleteFile();
                        _context.ImageUrls.Add(new ImageUrl()
                        {
                            Product = product,
                            ImagePath = thumbnailPath,
                            Thumbnail = 1
                        });
                    }

                    if (productModel.ImageFile != null)
                    {
                        foreach (var formFile in productModel.ImageFile)
                        {
                            var imagePath = await formFile.SaveImageAsync();
                            _context.ImageUrls.Add(new ImageUrl()
                            {
                                Product = product,
                                ImagePath = imagePath,
                                Thumbnail = 0,
                            });
                        }
                    }

                    var propertyInfos = typeof(Product).GetProperties();
                    if (product.TypeProducts.Count != productModel.TypeProducts.Count)
                    {
                        var typeProduct =  product.TypeProducts.Where(a =>
                            productModel.TypeProducts.All(p => p.Product.ProductName == a.Product.ProductName));
                        foreach (var product1 in typeProduct)
                        {
                            _context.TypeProducts.Remove(product1);
                            product.TypeProducts.Remove(product1);
                        }
                    }

                    foreach (var propertyInfo in propertyInfos)
                    {
                        if (propertyInfo.Name is "TypeProducts" or "Id" or "Sold")
                        {
                            continue;
                        }

                        var value = propertyInfo.GetValue(productModel);
                        if (value is null or IList { Count: 0 })
                        {
                            continue;
                        }

                        propertyInfo.SetValue(product, value);
                    }

                    foreach (var modelTypeProduct in productModel.TypeProducts)
                    {
                        bool isExist = false;
                        foreach (var typeProduct in product.TypeProducts)
                        {
                            if (typeProduct.TypeName == modelTypeProduct.TypeName)
                            {
                                isExist = true;
                                if (modelTypeProduct.ImageFile is { Length: > 0 })
                                {
                                    modelTypeProduct.ImagePath = await modelTypeProduct.ImageFile.SaveImageAsync();
                                }

                                if (typeProduct.Sizes.Count != modelTypeProduct.Sizes.Count)
                                {
                                    var size = typeProduct.Sizes
                                        .Where(p => modelTypeProduct.Sizes.All(a => a.SizeType != p.SizeType));
                                    foreach (var siz in size)
                                    {
                                        _context.Sizes.Remove(siz);
                                        typeProduct.Sizes.Remove(siz);
                                    }
                                }


                                foreach (var size in modelTypeProduct.Sizes)
                                {
                                    var sizeT = typeProduct.Sizes.FirstOrDefault(s => s.SizeType == size.SizeType);
                                    if (sizeT == null)
                                    {
                                        typeProduct.Sizes.Add(new Size
                                        {
                                            SizeType = size.SizeType,
                                            Quantity = size.Quantity
                                        });
                                    }
                                    else
                                    {
                                        sizeT.Quantity = size.Quantity;
                                    }
                                }
                            }
                        }

                        if (!isExist)
                        {
                            if (modelTypeProduct.ImageFile != null)
                                modelTypeProduct.ImagePath = await modelTypeProduct.ImageFile.SaveImageAsync();
                            foreach (var size in modelTypeProduct.Sizes)
                            {
                                size.TypeProductId = modelTypeProduct.Id;
                            }

                            product.TypeProducts.Add(modelTypeProduct);
                        }
                    }

                    await Task.Delay(100);
                    await _context.SaveChangesAsync();
                    return View("Edit", product);
                }
                else
                {
                    return RedirectToAction("Index", "Product");
                }
            }

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Detail(string? id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index", "Product");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return View(product);
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

            var targetProduct = await _context.Products
                .Include(p => p.ImageUrls)
                .Include(p => p.TypeProducts)
                .FirstOrDefaultAsync(p => p.Id == id);


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