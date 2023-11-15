using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;

public class ProductModel
{
    public string Id { get; set; }

    public string Price { get; set; }
    // Thêm các thuộc tính khác nếu cần
}

public class CheckoutController : Controller
{
    private ShoppingContext _context;
    private UserManager<UserModel> _userManager;

    public CheckoutController(ShoppingContext context, UserManager<UserModel> userManager)
    {
        _context = context;
        _userManager = userManager;
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

    //Viết action method CheckOut(), thông tin đơn hàng sẽ được lưu vào bảng Bill và BillItem
    [HttpPost]
    public async Task<IActionResult> CheckOut(List<string> cartItemID) //truyền vào 1 danh sách product từ checkout
    {
        var user = await _userManager.GetUserAsync(User); //lấy user ra
        Bill bill = new Bill();
        bill.UserId = user.Id;
        bill.Quantity = 0;
        foreach (var cartItem in cartItemID)
        {
            var cart = _context.CartItems.FirstOrDefault(a => a.Id == cartItem);
            if (cart != null)
            {
                bill.Quantity++;
                BillItem billItem = new BillItem();
                billItem.TypeProductId = cart.TypeProductId;
                billItem.Quantity = cart.Quantity;
                billItem.Total = cart.Quantity * cart.TypeProduct.Product.Price;
                billItem.SizeName = cart.SizeType;
                bill.BillItems.Add(billItem);
                bill.Total += billItem.Total;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            _context.CartItems.Remove(cart);
        }
        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();

        /*_context.SaveChanges();*/
        /*cartItem.ClearCart();*/
        return RedirectToAction("Index", "Checkout");
    }
}