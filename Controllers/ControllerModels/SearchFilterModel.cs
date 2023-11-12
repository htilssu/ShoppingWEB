namespace ShoppingWEB.Controllers.ControllerModels;

public class SearchFilterModel
{
    public string? Location { get; set; }
    public double? PriceFrom { get; set; }
    public double? PriceTo { get; set; }
    public string? IsDiscount { get; set; }
}