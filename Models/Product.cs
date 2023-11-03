using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Models;

public partial class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Display(Name = "Tên Sản Phẩm")] public string? ProductName { get; set; }

    [Display(Name = "Giá")] public double? Price { get; set; }

    [Display(Name = "Giảm Giá (%)")] public double? DiscountPercent { get; set; }

    [Display(Name = "Mô Tả Ngắn")] public string? ShortDescription { get; set; }

    [Display(Name = "Mô Tả Chi Tiết")] public string? LongDescription { get; set; }

    [Display(Name = "Công Khai")] public sbyte? Publish { get; set; }

    [Display(Name = "Mặt Hàng")] public string? CategoryId { get; set; }

    public int Rating { get; set; } = 0;

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }


    public virtual ICollection<ImageUrl> ImageUrls { get; set; } = new List<ImageUrl>();

    [Display(Name = "Loại Sản Phẩm")]
    public virtual ICollection<TypeProduct> TypeProducts { get; set; } = new List<TypeProduct>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}