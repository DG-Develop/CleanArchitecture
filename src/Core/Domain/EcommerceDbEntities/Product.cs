using System;
using System.Collections.Generic;

namespace ECommerce.Domain.EcommerceDbEntities;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public decimal Price { get; set; }

    public int IdCoin { get; set; }

    public int IdCatProductType { get; set; }

    public bool Active { get; set; }

    public virtual CatCoin IdCoinNavigation { get; set; } = null!;
}
