using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Cart
{
    public string Id { get; set; } = null!;

    public string? CustomerId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual AspNetUser? Customer { get; set; }
}
