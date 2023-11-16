using ShoppingWEB.Models;

namespace ShoppingWEB.Areas.Admin.Models;

public class ImageModel : ImageUrl
{

    public IFormFile ImageFile { get; set; } = null!;
}