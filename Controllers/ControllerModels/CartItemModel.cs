using Microsoft.CodeAnalysis.Scripting;
using ShoppingWEB.Models;

namespace ShoppingWEB.Controllers.ControllerModels;

public class CartItemModel : CartItem
{
    public string? TypeProductId { get; set; }
    public string? SizeType { get; set; }
}