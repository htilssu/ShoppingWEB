﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWEB.Models;

public partial class TypeProduct : IEnumerable
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? ProductId { get; set; } = null!;

    public string? TypeName { get; set; }


    public string? ImagePath { get; set; }

    [NotMapped] public IFormFile ImageFile { get; set; } = null!;
    public virtual Product? Product { get; set; }
    public ICollection<Size> Sizes { get; set; } = new List<Size>();
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public IEnumerator GetEnumerator()
    {
        throw new NotImplementedException();
    }
}