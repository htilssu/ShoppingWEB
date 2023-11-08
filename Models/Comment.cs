using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Comment
{
    public string Id { get; set; } = null!;

    public string? Content { get; set; }

    public string? UserId { get; set; }

    public DateTime? Time { get; set; }

    public string? TypeProductId { get; set; }

    public string? Material { get; set; }

    public string? Color { get; set; }

    public string? LikeDetail { get; set; }
    public UserModel? User { get; set; }
    public TypeProduct? TypeProduct { get; set; }
}
