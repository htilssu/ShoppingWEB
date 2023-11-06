using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Models;

public partial class Category
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Display(Name = "Tên Loại Hàng")] public string? CategoryName { get; set; }
    [Display(Name = "Ảnh")] public string? ImagePath { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
