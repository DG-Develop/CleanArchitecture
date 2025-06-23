using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Domain.ValueObject.Products
{
    public class ProductDetailValueJoin
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PriceCoin { get; set; }
        public string Descrip { get; set; }
    }
}
