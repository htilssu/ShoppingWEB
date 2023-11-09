using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Coupon
{
    public string Id { get; set; } = null!;

    public string? Code { get; set; }

    public string? CouponDescription { get; set; }

    public int? DiscountValue { get; set; }

    public int? Limited { get; set; }

    public DateTime? StartAt { get; set; }

    public DateTime? EndAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
