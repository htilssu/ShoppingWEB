using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Size
{
    public string TypeProductId { get; set; } = null!;

    public int SizeNumber { get; set; }
    public int Quantity { get; set; } = 0;

    public virtual TypeProduct? TypeProduct { get; set; }
}