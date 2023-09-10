
using Microsoft.AspNetCore.Identity;

namespace Shoppe_Clone.Models;

public class RoleModel : IdentityRole
{
    public string RoleId { get; set; }
    public string Name { get; set; }
}