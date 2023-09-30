using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Views.Login;

public class LoginModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
    public string? Username { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    public string? Password { get; set; }

    [Display(Name = "Ghi nhớ đăng nhập?")] public bool Remember { get; set; }
}