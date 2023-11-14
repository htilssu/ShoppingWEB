using System.Web;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers
{
    
    public class ProductModel
    {
        public string? Id { get; set; }

        public string? Price { get; set; }
        // Thêm các thuộc tính khác nếu cần
    }

    public class CheckoutController : Controller
    {
        private ShoppingContext _context;

        public CheckoutController(ShoppingContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy tham số từ URL
            var selectedProductsParam = Request.Query["selectedProducts"];
            
            if (!string.IsNullOrEmpty(selectedProductsParam))
            {
                // Giải mã JSON từ tham số selectedProducts
                List<ProductModel> selectedProducts =
                    JsonConvert.DeserializeObject<List<ProductModel>>(HttpUtility.UrlDecode(selectedProductsParam));
                
                // Thực hiện các bước xử lý cần thiết với thông tin sản phẩm đã chọn
                var listCart = new List<CartItem>();
                foreach (var selectedProduct in selectedProducts)
                {
                    listCart.Add(_context.CartItems.FirstOrDefault(p => p.Id == selectedProduct.Id)!);
                }

                if (listCart.Count == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }
                ViewBag.Info = selectedProducts;

                return View(listCart);
            }

         
            return RedirectToAction("Index", "Cart");
        }
    }
}