using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Orders.DTOs
{
    public class ExerciseDto
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int NumberPurchased { get; set; }
    }
}