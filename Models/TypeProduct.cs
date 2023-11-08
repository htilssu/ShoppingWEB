using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWEB.Models;

public partial class TypeProduct
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? ProductId { get; set; }

    public string? TypeName { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Product? Product { get; set; }

    public virtual ICollection<Size> Sizes { get; set; } = new List<Size>();
    [NotMapped] public IFormFile? ImageFile { get; set; }
    public IEnumerable<Comment> Comments { get; set; } = new List<Comment>();
}