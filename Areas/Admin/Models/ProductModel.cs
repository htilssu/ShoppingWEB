using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Models;

public class ProductModel : Product
{
    public IFormFile ImageFile { get; set; } = null!;
}