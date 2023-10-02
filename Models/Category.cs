using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWEB.Models;

public class Category
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("category_name")] public string? CategoryName { get; set; }

    [Column("image_path")] public string? ImagePath { get; set; }

    [Column("created_at")] public DateTime? CreatedAt { get; set; }

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; }
}