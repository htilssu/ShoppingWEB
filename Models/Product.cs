using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoppingWEB.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Column("product_name")] public string? ProductName { get; set; }

    public double? Price { get; set; }

    [Column("discount_price")] public double? DiscountPrice { get; set; }

    [Column("in_stock")] public int? InStock { get; set; }

    [Column("short_description")] public string? ShortDescription { get; set; }

    [Column("product_description")] public string? ProductDescription { get; set; }

    public sbyte? Published { get; set; }

    [Display(Name = "Category")]
    [Column("category_id")]
    public string? CategoryId { get; set; }


    [Column("created_at")] public DateTime? CreatedAt { get; set; }

    [Column("updated_at")] public DateTime? UpdatedAt { get; set; }
}