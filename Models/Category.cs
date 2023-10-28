using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Category
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}