using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Models;

public class ProductModel : Product
{
    public List<IFormFile>? ImageFile { get; set; }
    public IFormFile? Thumbnail { get; set; }
}