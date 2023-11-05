using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Seller
{
    public string Id { get; set; } = null!;

    public string? SellerName { get; set; }

    public int? ReplyPercent { get; set; }

    public DateTime? JoinAt { get; set; }

    public int? Follower { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
