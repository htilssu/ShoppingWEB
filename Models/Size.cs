using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Size
{
    public string? TypeProductId { get; set; } = null!;

    public string? SizeType { get; set; } = null!;

    public int? Quantity { get; set; }

    public virtual TypeProduct? TypeProduct { get; set; } = null!;
}