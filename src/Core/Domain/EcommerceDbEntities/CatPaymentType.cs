using System;
using System.Collections.Generic;

namespace ECommerce.Domain.EcommerceDbEntities;

public partial class CatPaymentType
{
    public int IdPaymentType { get; set; }

    public string Description { get; set; } = null!;

    public bool Active { get; set; }

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
