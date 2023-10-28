using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class DeliveryProvIder
{
    public string Id { get; set; } = null!;

    public string? ProvIderName { get; set; }

    public double? Price { get; set; }
}
