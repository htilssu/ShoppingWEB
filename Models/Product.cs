using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string? ProductName { get; set; }

    public double? DiscountPercent { get; set; }

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public sbyte? Publish { get; set; }

    public string? CategoryId { get; set; }

    public DateTime? CreateAt { get; set; }

    public DateTime? UpdateAt { get; set; }

    public int Rating { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<ImageUrl> ImageUrls { get; set; } = new List<ImageUrl>();

    public virtual ICollection<TypeProduct> TypeProducts { get; set; } = new List<TypeProduct>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();
}
