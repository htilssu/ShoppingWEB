using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ShoppingWEB.Models;

[PrimaryKey(nameof(ImagePath), nameof(ProductId))]
[Table("ImageURL")]
public class ImageUrl
{
    [Column("image_path")] public string ImagePath { get; set; } = null!;
    [Column("product_id")] public string ProductId { get; set; } = null!;

    public bool? Thumnail { get; set; }
}