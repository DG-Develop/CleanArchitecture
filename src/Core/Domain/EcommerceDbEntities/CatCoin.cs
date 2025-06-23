using System;
using System.Collections.Generic;

namespace ECommerce.Domain.EcommerceDbEntities;

public partial class CatCoin
{
    public int IdCoin { get; set; }

    public string Name { get; set; } = null!;

    public string Symbol { get; set; } = null!;

    public string IsoCode { get; set; } = null!;

    public bool? Active { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
