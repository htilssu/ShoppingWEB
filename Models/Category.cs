namespace ShoppingWEB.Models;

public class Category
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? CategoryName { get; set; }

    public string? ImagePath { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }
}