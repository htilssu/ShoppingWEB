namespace ShoppingWEB.Models;

public class Seller
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string? SellerName { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}