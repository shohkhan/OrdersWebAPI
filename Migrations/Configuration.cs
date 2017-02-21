namespace Orders.Migrations
{
    using Orders.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Orders.Models.OrdersContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Orders.Models.OrdersContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            Product pr1 = new Product { Name = "The Lord of the Rings", Description = "By J.R.R. Tolkien", Price = 15 };
            Product pr2 = new Product { Name = "The Godfather", Description = "By Mario Puzo", Price = 10 };
            Product pr3 = new Product { Name = "The Martian", Description = "By Andy Weir", Price = 9 };
            Product pr4 = new Product { Name = "The Name of the Wind", Description = "By Patrick Rothfuss", Price = 12 };
            Product pr5 = new Product { Name = "Harry Potter and the Sorcerer's Stone", Description = "By J.K. Rowling", Price = 13 };
            Product pr6 = new Product { Name = "To Kill a Mockingbird", Description = "By Harper Lee", Price = 7 };

            Category ct1 = new Category { Name = "Fantasy" };
            Category ct2 = new Category { Name = "Adventure" };
            Category ct3 = new Category { Name = "Drama" };
            Category ct4 = new Category { Name = "Sci-Fi" };
            Category ct5 = new Category { Name = "Kids" };
            Category ct6 = new Category { Name = "Crime" };

            ProductCategory pc1 = new ProductCategory { Product = pr1, Category = ct1 };
            ProductCategory pc2 = new ProductCategory { Product = pr1, Category = ct2 };
            ProductCategory pc3 = new ProductCategory { Product = pr2, Category = ct6 };
            ProductCategory pc4 = new ProductCategory { Product = pr2, Category = ct3 };
            ProductCategory pc5 = new ProductCategory { Product = pr3, Category = ct4 };
            ProductCategory pc6 = new ProductCategory { Product = pr4, Category = ct1 };
            ProductCategory pc7 = new ProductCategory { Product = pr4, Category = ct2 };
            ProductCategory pc8 = new ProductCategory { Product = pr5, Category = ct1 };
            ProductCategory pc9 = new ProductCategory { Product = pr5, Category = ct5 };
            ProductCategory pc10 = new ProductCategory { Product = pr5, Category = ct2 };
            ProductCategory pc11 = new ProductCategory { Product = pr6, Category = ct6 };

            context.ProductCategories.Add(pc1);
            context.ProductCategories.Add(pc2);
            context.ProductCategories.Add(pc3);
            context.ProductCategories.Add(pc4);
            context.ProductCategories.Add(pc5);
            context.ProductCategories.Add(pc6);
            context.ProductCategories.Add(pc7);
            context.ProductCategories.Add(pc8);
            context.ProductCategories.Add(pc9);
            context.ProductCategories.Add(pc10);

            //*******************************************************************************

            OrderStatus os1 = new OrderStatus { Name = "Not Submitted" };
            OrderStatus os2 = new OrderStatus { Name = "Waiting For Delivery" };
            OrderStatus os3 = new OrderStatus { Name = "On It's Way" };
            OrderStatus os4 = new OrderStatus { Name = "Delivered" };

            var td = DateTime.Today;

            ShopOrder so1 = new ShopOrder { OrderStatus = os1, SubmissionDate = td };
            ShopOrder so2 = new ShopOrder { OrderStatus = os2, SubmissionDate = td.AddDays(-4) };
            ShopOrder so3 = new ShopOrder { OrderStatus = os3, SubmissionDate = td.AddDays(-8) };
            ShopOrder so4 = new ShopOrder { OrderStatus = os4, SubmissionDate = td.AddDays(-12) };

            Customer customer = new Customer
            {
                FirstName = "John",
                LastName = "Doe",
                Address = "Birmingham, AL",
                Email = "john@doe.com",
                Phone = "555-123-4567",
                Orders = new List<ShopOrder> { so1, so2, so3, so4 }
            };

            context.Customers.Add(customer);

            //********************************************************************************************

            ProductOrder po1 = new ProductOrder { Product = pr1, ShopOrder = so1, Quantity = 1 };
            ProductOrder po2 = new ProductOrder { Product = pr2, ShopOrder = so1, Quantity = 2 };
            ProductOrder po3 = new ProductOrder { Product = pr2, ShopOrder = so2, Quantity = 3 };
            ProductOrder po4 = new ProductOrder { Product = pr3, ShopOrder = so3, Quantity = 1 };
            ProductOrder po5 = new ProductOrder { Product = pr4, ShopOrder = so4, Quantity = 1 };
            ProductOrder po6 = new ProductOrder { Product = pr3, ShopOrder = so2, Quantity = 2 };

            context.ProductOrders.Add(po1);
            context.ProductOrders.Add(po2);
            context.ProductOrders.Add(po3);
            context.ProductOrders.Add(po4);
            context.ProductOrders.Add(po5);
            context.ProductOrders.Add(po6);
        }
    }
}
