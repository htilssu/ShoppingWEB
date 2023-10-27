using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Order
{
    public string Id { get; set; } = null!;

    public string? CustomerId { get; set; }

    public string? PaymentId { get; set; }

    public string? CouponId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual PaymentInfo? Payment { get; set; }
}