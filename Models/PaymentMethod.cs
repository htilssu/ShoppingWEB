﻿using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class PaymentMethod
{
    public string Id { get; set; } = null!;

    public string? PaymentName { get; set; }
}