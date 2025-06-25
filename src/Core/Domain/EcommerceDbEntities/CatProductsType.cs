using System;
using System.Collections.Generic;

namespace ECommerce.Domain.EcommerceDbEntities;

public partial class CatProductsType
{
    public int IdCatProductType { get; set; }

    public string Descrip { get; set; } = null!;

    public bool Active { get; set; }
}
