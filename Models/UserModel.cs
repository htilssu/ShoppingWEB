using Microsoft.AspNetCore.Identity;

namespace ShoppingWEB.Models;

public class UserModel : IdentityUser
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime BirthDay { get; set; }
    public bool Gender { get; set; }
}