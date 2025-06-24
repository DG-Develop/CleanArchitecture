using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.ValueObject.Products
{
    public class ProductResultDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string PriceCoin { get; set; }
        //public string ProductType { get; set; }
    }
}
