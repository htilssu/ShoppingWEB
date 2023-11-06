using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Models;

public class ProductModel : Product
{
    public List<IFormFile>? ImageFile { get; set; } = new List<IFormFile>();
    public IFormFile? Thumbnail { get; set; }
}