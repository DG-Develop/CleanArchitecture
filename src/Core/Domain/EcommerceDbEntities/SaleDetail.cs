using System;
using System.Collections.Generic;

namespace ECommerce.Domain.EcommerceDbEntities;

public partial class SaleDetail
{
    public int IdSaleDetails { get; set; }

    public int IdSale { get; set; }

    public int IdProduct { get; set; }

    public int Amount { get; set; }

    public decimal PriceOrigin { get; set; }

    public decimal Total { get; set; }

    public bool Active { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual Sale IdSaleNavigation { get; set; } = null!;
}
