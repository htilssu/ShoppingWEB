using System.Globalization;
using System.Text;
using ShoppingWEB.Models;

namespace ShoppingWEB.Extension_Method;

public static class Extension
{
    public static string GetWebPath(this Category category)
    {
        var path = category.ImagePath;
        return path != null ? path[path.IndexOf('\\')..].Replace(@"\", "/") : "";
    }

    public static void DeleteFile(this string path)
    {
        path = "wwwroot" + path.Replace("/", "\\");

        try
        {
            File.Delete(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static string GetWebPath(this string path)
    {
        return path[path.IndexOf('\\')..].Replace(@"\", "/");
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

    public static async Task<string> SaveImageAsync(this IFormFile? formFile)
    {
        var filePath = "";
        if (formFile is { Length: > 0 })
            try
            {
                var uploadFolder = Path.Combine("wwwroot", "uploads");
                var uniqueFileName = Guid.NewGuid() + "_" + formFile.FileName;
                filePath = Path.Combine(uploadFolder, uniqueFileName);
                await using var stream = new FileStream(filePath, FileMode.Create);
                await formFile.CopyToAsync(stream);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        filePath = filePath[(filePath.IndexOf("uploads", StringComparison.Ordinal) - 1)..]
            .Replace(@"\", "/");
        return filePath;
    }
}