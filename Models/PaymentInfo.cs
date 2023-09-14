using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class PaymentInfo
{
    public string Id { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
