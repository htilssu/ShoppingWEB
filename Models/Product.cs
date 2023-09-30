namespace ShoppingWEB.Models;

public class Product
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public double? DiscountPrice { get; set; }

    public int? InStock { get; set; }

    public string? ShortDescription { get; set; }

    public string? ProductDescription { get; set; }

    public sbyte? Published { get; set; }

    public string? CategoryId { get; set; }


    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}