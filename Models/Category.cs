﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace ShoppingWEB.Models;


public partial class Category
{
    public string Id { get; set; } = null!;

    [Display(Name = "Loại Mặt Hàng")]
    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
