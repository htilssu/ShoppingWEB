using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Product
{
    public string Id { get; set; } = null!;

    public string? CategoryId { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public double? DiscountPercent { get; set; }

    public string? ShortDescription { get; set; }

    public string? LongDescription { get; set; }

    public byte? Publish { get; set; }

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