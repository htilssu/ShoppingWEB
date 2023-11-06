using System;
using System.Collections.Generic;

namespace ShoppingWEB.Models;

public partial class Bill
{
    public string Id { get; set; } = null!;

    public string ItemId { get; set; } = null!;

    public double Total { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public virtual ICollection<Bill> InversePaymentMethodNavigation { get; set; } = new List<Bill>();

    public virtual CartItem Item { get; set; } = null!;

    public virtual Bill PaymentMethodNavigation { get; set; } = null!;
}