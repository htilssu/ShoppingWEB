using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShoppingWEB.Models;
public class LoginModel
{

    [Required(ErrorMessage = "Vui lòng nhập tên người dùng")]
    public string? Username { get; set; }

    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    public string? Password { get; set; }

    public bool Remember { get; set; }
    
}