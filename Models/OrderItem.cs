using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class OrderItem
{
    public string Id { get; set; } = null!;

    public string? OrderId { get; set; }

    public string? DeliveryProviderId { get; set; }

    public int? Quanity { get; set; }

    public double? Total { get; set; }

    public virtual DeliveryProvider? DeliveryProvider { get; set; }

    public virtual Order? Order { get; set; }
}
