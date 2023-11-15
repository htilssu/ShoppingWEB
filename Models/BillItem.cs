namespace ShoppingWEB.Models;

public partial class BillItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string? BillId { get; set; }

    public string? TypeProductId { get; set; }

    public string? SizeName { get; set; }

    public int? Quantity { get; set; }

    public double? Total { get; set; }

    public virtual Bill? Bill { get; set; }

    public virtual TypeProduct? TypeProduct { get; set; }
}