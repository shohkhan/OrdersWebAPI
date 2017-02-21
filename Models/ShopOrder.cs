using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class ShopOrder
    {
        public int ShopOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime SubmissionDate { get; set; }
        public virtual ICollection<ProductOrder> Products { get; set; }
    }
}