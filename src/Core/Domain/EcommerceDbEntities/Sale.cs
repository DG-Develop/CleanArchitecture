using System;
using System.Collections.Generic;

namespace ECommerce.Domain.EcommerceDbEntities;

public partial class Sale
{
    public int IdSale { get; set; }

    public int Amount { get; set; }

    public decimal Total { get; set; }

    public int IdPaymentType { get; set; }

    public DateTime Date { get; set; }

    public bool Active { get; set; }

    public virtual CatPaymentType IdPaymentTypeNavigation { get; set; } = null!;

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
