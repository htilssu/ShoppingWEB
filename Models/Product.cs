using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Models;

public partial class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Display(Name = "Loại Sản Phẩm")] public string? CategoryId { get; set; }

    [Display(Name = "Tên Sản Phẩm")] public string? ProductName { get; set; }

    [Display(Name = "Giá")] public double? Price { get; set; }

    [Display(Name = "Giảm Giá(%)")] public double? DiscountPercent { get; set; }

    [Display(Name = "Mô Tả Ngắn")] public string? ShortDescription { get; set; }

    [Display(Name = "Mô Tả Chí Tiết")] public string? LongDescription { get; set; }

    [Display(Name = "Công Khai")] public byte? Publish { get; set; }

    public int? Rating { get; set; }

    public int? Sold { get; set; }

    public string? SellerId { get; set; }

    public string? Style { get; set; }

    public string? MadeIn { get; set; }

    public string? Material { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ImageUrl> ImageUrls { get; set; } = new List<ImageUrl>();

    public virtual Seller? Seller { get; set; }

    public virtual ICollection<TypeProduct> TypeProducts { get; set; } = new List<TypeProduct>();
}