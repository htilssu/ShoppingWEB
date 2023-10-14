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
}