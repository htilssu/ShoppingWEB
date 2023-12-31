using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

namespace ShoppingWEB.Models;

public class UserModel : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDay { get; set; }
    public bool? Gender { get; set; }
    public string AvtPath { get; set; } = "/avatars/default-avatar.png";
}