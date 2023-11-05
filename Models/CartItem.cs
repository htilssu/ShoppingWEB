using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class CartItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? CartId { get; set; }

    public int? Quantity { get; set; }

    public string? TypeProductId { get; set; }

    public string? SizeType { get; set; }

    public TypeProduct? TypeProduct { get; set; }
}