
using Microsoft.AspNetCore.Identity;

namespace ShoppingWEB.Models;

public class RoleModel : IdentityRole
{
    public string RoleId { get; set; }
    public string Name { get; set; }
}