using System.Text.RegularExpressions;

namespace ShoppingWEB.Extension_Method;

public abstract class Valid
{
    public static bool IsValidPhoneNumber(string? phoneNumber)
    {
        const string phoneRegex = @"^0\d{9}$";
        return Regex.IsMatch(phoneNumber ?? throw new ArgumentNullException(nameof(phoneNumber)), phoneRegex);
    }

    public static bool IsValidEmail(string? email)
    {
        const string emailRegex = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
        return Regex.IsMatch(email ?? throw new ArgumentNullException(nameof(email)), emailRegex);
    }
}