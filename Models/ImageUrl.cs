using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class ImageUrl
{
    public string ImagePath { get; set; } = null!;

    public string? ProductId { get; set; }

    public byte? Thumbnail { get; set; }

    public virtual Product? Product { get; set; }
}