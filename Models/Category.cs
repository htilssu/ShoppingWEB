using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Category
{
    public string Id { get; set; } = null!;

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
