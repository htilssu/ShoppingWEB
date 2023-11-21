using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers;


public class CheckoutController : Controller
{
    private readonly ShoppingContext _context;

    private readonly UserManager<UserModel> _userManager;

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
            var selectedProducts =
                JsonConvert.DeserializeObject<List<Product>>(HttpUtility.UrlDecode(selectedProductsParam));

            // Thực hiện các bước xử lý cần thiết với thông tin sản phẩm đã chọn
            var listCart = new List<CartItem>();
            if (selectedProducts != null)
            {
                foreach (var selectedProduct in selectedProducts)
                {
                    var item = _context.CartItems.FirstOrDefault(p => p.Id == selectedProduct.Id);
                    if (item == null)
                    {
                        return NotFound();
                    }

                    listCart.Add(item);
                }

                if (listCart.Count == 0)
                {
                    return RedirectToAction("Index", "Cart");
                }

                ViewBag.Info = selectedProducts;
            }

            return View(listCart);
        }


        return RedirectToAction("Index", "Cart");
    }


    //Viết action method CheckOut(), thông tin đơn hàng sẽ được lưu vào bảng Bill và BillItem
    [HttpPost]
    public async Task<IActionResult>
        CheckOut(List<string> cartItemId) //truyền vào 1 danh sách product từ checkout
    {
        var user = await _userManager.GetUserAsync(User); //lấy user ra
        if (user == null)
        {
            return NotFound();
        }
        Bill bill = new Bill
        {
            UserId = user.Id,
            Quantity = 0,
            Total = 0,
            Date = DateTime.Now
        };
        foreach (var cart in cartItemId.Select(cartItem => _context.CartItems.Include(item => item.TypeProduct)
                     .ThenInclude(typeProduct => typeProduct!.Sizes).Include(item => item.TypeProduct)
                     .ThenInclude(typeProduct => typeProduct!.Product).FirstOrDefault(a => a.Id == cartItem)))
        {
            if (cart != null)
            {
                var size = cart.TypeProduct?.Sizes.FirstOrDefault(s => s.SizeType == cart.SizeType);

                if (size != null) size.Quantity -= cart.Quantity;
                if (cart.TypeProduct?.Product != null)
                {
                    cart.TypeProduct.Product.Sold += cart.Quantity;
                    bill.Quantity++;
                    var billItem = new BillItem
                    {
                        TypeProductId = cart.TypeProductId,
                        Quantity = cart.Quantity,
                        Total = cart.Quantity * cart.TypeProduct.Product.Price,
                        SizeName = cart.SizeType
                    };
                    bill.BillItems.Add(billItem);
                    bill.Total += billItem.Total;
                }


                _context.CartItems.Remove(cart);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        _context.Bills.Add(bill);
        await _context.SaveChangesAsync();

        /*_context.SaveChanges();*/
        /*cartItem.ClearCart();*/
        return RedirectToAction("Index", "Checkout");
    }
}