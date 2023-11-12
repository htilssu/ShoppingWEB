namespace ShoppingWEB.Models;

public partial class BillItem
{
    public string Id { get; set; } = null!;

    public string? BillId { get; set; }

    public string? TypeProductId { get; set; }

    public string? SizeName { get; set; }

    public int? Quantity { get; set; }

    public virtual aaa.Bill? Bill { get; set; }

    public virtual aaa.TypeProduct? TypeProduct { get; set; }
}
