using ShoppingWEB.Models;

namespace ShoppingWEB.Extension_Method;

public static class CategoryFn
{
    public static string GetWebPath(this Category category)
    {
        var path = category.ImagePath;
        return path![path.IndexOf('\\')..].Replace(@"\", "/");
    }
}