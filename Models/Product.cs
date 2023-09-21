using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public double? DicountPrice { get; set; }

    public int? InStock { get; set; }

    public string? ShortDescription { get; set; }

    public string? ProductDescription { get; set; }

    public sbyte? Published { get; set; }

    public string? CategoryId { get; set; }

    public string? SellerId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<ImageUrl> ImageUrls { get; set; } = new List<ImageUrl>();

    public virtual UserModel? Seller { get; set; }

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
