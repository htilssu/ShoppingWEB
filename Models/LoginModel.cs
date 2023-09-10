using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

public class LoginModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
    [MaxLength(50)]
    [MinLength(5)]
    public string Username { get; set; }
    
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    public string Password { get; set; }
}