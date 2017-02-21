using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.DTOs
{
    public class OrderDto
    {
        public string OrderStatus { get; set; }
        public DateTime SubmissionDate { get; set; }
        public List<ProductDto> Products { get; set; }

    }
}