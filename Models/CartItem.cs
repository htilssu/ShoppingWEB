using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class CartItem
{
    public string Id { get; set; } = null!;

    public string? ProductId { get; set; }

    public string? CartId { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual Cart? Cart { get; set; }

    public virtual Product? Product { get; set; }
}