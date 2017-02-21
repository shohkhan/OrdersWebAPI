using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public virtual ICollection<ProductCategory> Categories { get; set; }
        public virtual ICollection<ShopOrder> Orders { get; set; }
    }
}