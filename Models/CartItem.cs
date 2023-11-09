using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class CartItem
{
    public string Id { get; set; } = null!;

    public string? CartId { get; set; }

    public int? Quantity { get; set; }

    public string? TypeProductId { get; set; }

    public string? SizeType { get; set; }

    public virtual Cart? Cart { get; set; }

    public virtual TypeProduct? TypeProduct { get; set; }
}
