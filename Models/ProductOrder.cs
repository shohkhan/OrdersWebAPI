using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class ProductOrder
    {
        [Key, Column(Order = 0)]
        public int ProductId { get; set; }
        [Key, Column(Order = 1)]
        public int ShopOrderId { get; set; }

        public virtual Product Product { get; set; }
        public virtual ShopOrder ShopOrder { get; set; }
        public int Quantity { get; set; }
    }
}