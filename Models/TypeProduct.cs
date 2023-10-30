using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class TypeProduct
{
    public string Id { get; set; } = null!;

    public string ProductId { get; set; } = null!;

    public string? TypeName { get; set; }

    public double? Price { get; set; }

    public string? ImagePath { get; set; }

    public virtual Product Product { get; set; } = null!;
    public ICollection<Size> Sizes { get; set; } = new List<Size>();
}