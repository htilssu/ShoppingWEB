using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class DeliveryType
{
    public string Id { get; set; } = null!;

    public string? ProviderName { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();
}