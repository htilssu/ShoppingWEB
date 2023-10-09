namespace ShoppingWEB.Areas.Admin.Models;

public class CategoryModel
{
    public string? CategoryName { get; set; }

    public IFormFile ImageFile { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}