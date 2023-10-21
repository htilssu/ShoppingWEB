using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class DeliveryProvider
{
    public string Id { get; set; } = null!;

    public string? DeliveryproviderName { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
