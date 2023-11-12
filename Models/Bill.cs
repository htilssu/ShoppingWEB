namespace ShoppingWEB.Models;

public partial class Bill
{
    public string Id { get; set; } = null!;

    public string? TypeProductId { get; set; }

    public string? UserId { get; set; }

    public double? Total { get; set; }

    public string? PaymentMethod { get; set; }

    public int? Quantity { get; set; }

    public string? DeliveryId { get; set; }

    public virtual DeliveryType? Delivery { get; set; }

    public virtual PaymentMethod? PaymentMethodNavigation { get; set; }

    public virtual TypeProduct? TypeProduct { get; set; }
    public virtual ICollection<BillItem> BillItems { get; set; } = new List<BillItem>();
}