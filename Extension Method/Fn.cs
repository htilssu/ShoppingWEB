using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ShoppingWEB.Models;

namespace ShoppingWEB.Extension_Method;

public static class Extension
{
    public static string GetWebPath(this Category category)
    {
        var path = category.ImagePath;
        return path![path.IndexOf('\\')..].Replace(@"\", "/");
    }

    public static string GetWebPath(this ImageUrl image)
    {
        var path = image.ImagePath;
        return path![path.IndexOf('\\')..].Replace(@"\", "/");
    }

    public static string GetWebPath(this string path)
    {
        return path![path.IndexOf('\\')..].Replace(@"\", "/");
    }

    public static string RemoveSpecialMark(this string text)
    {
        var textNormalize = text.Normalize();
        var newText = new StringBuilder();
        foreach (var c in textNormalize.Where(c =>
                     CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark))
            newText.Append(c);

        return newText.ToString();
    }
}