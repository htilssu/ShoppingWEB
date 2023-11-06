using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Models;

public partial class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Display(Name = "Đại Lý Bán Hàng")] public string? SellerId { get; set; }
    [Display(Name = "Tên Sản Phẩm")] public string? ProductName { get; set; }

    [Display(Name = "Giá")] public double? Price { get; set; }

    [Display(Name = "Giảm Giá (%)")] public double? DiscountPercent { get; set; } = 0;

    [Display(Name = "Mô Tả Ngắn")] public string? ShortDescription { get; set; }

    [Display(Name = "Mô Tả Chi Tiết")] public string? LongDescription { get; set; }

    [Display(Name = "Công Khai")] public sbyte? Publish { get; set; } = 1;

    [Display(Name = "Mặt Hàng")] public string? CategoryId { get; set; }


    public int? Rating { get; set; } = 0;

    public string? MadeIn { get; set; }
    public string? Style { get; set; }

    public string? Material { get; set; }
    public int? Sold { get; set; } = 0;


    public virtual Category? Category { get; set; }

    public virtual ICollection<ImageUrl> ImageUrls { get; set; } = new List<ImageUrl>();

    public virtual Seller? Seller { get; set; }

    [Display(Name = "Loại Sản Phẩm")]
    public virtual ICollection<TypeProduct> TypeProducts { get; set; } = new List<TypeProduct>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}