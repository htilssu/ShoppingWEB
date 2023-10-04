namespace ShoppingWEB.Areas.Admin.Models;

public class ImageModel
{
    public string? ProductId { get; set; } = null!;

    public string? ImagePath { get; set; }

    public IFormFile ImageFile { get; set; }
    public bool? Thumnail { get; set; }
}