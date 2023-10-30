using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class DeliveryProvider
{
    public string Id { get; set; } = null!;

    public string? ProviderName { get; set; }

    public double? Price { get; set; }
}