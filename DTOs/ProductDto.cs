using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.DTOs
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double UnitPrice { get; set; }
        public double TotalPrice { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public List<string> Categories { get; set; }
    }
}