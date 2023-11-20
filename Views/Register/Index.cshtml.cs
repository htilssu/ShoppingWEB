using System.ComponentModel.DataAnnotations;

namespace ShoppingWEB.Views.Register;

public class RegistModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
    public string? UserName { get; set; }

    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Vui lòng nhập Email")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập số điện thoại")]
    [DataType(DataType.PhoneNumber)]
    public string? NumberPhone { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(8,ErrorMessage = "Độ dài phải hơn 8 ký tự")]
    [MaxLength(32,ErrorMessage = "Độ dài không quá 32 ký tự")]
    public string? Password { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [Compare("Password", ErrorMessage = "Mật khẩu không khớp")]
    public string? RePassword { get; set; }
}