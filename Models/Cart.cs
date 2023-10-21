﻿using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Cart
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? CustomerId { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}