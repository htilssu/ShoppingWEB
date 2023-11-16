using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Areas.Admin.Models;

public class CategoryModel
{
    [Required(ErrorMessage = "Không được để trống")]
    public string? CategoryName { get; set; }


    [Display(Name = "Image")]
    [Required(ErrorMessage = "Hãy chọn ảnh")]
    public IFormFile? ImageFile { get; set; }
}