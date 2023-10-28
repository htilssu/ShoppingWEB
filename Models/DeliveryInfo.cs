using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class DeliveryInfo
{
    public string Id { get; set; } = null!;

    public string? ReceiverId { get; set; }

    public string? Street { get; set; }

    public string? Ward { get; set; }

    public string? District { get; set; }

    public string? City { get; set; }

    public string? PhoneNumber { get; set; }

    public bool? Default { get; set; }

    public virtual AspNetUser? Receiver { get; set; }
}
