using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Orders.Models
{
    public class OrdersContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public OrdersContext() : base("name=OrdersContext")
        {
        }

        public System.Data.Entity.DbSet<Orders.Models.Customer> Customers { get; set; }

        public System.Data.Entity.DbSet<Orders.Models.ShopOrder> ShopOrders { get; set; }

        public System.Data.Entity.DbSet<Orders.Models.ProductCategory> ProductCategories { get; set; }

        public System.Data.Entity.DbSet<Orders.Models.Category> Categories { get; set; }

        public System.Data.Entity.DbSet<Orders.Models.Product> Products { get; set; }

        public System.Data.Entity.DbSet<Orders.Models.ProductOrder> ProductOrders { get; set; }

        public System.Data.Entity.DbSet<Orders.Models.OrderStatus> OrderStatus { get; set; }
    }
}
