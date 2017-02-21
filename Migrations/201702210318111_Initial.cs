namespace Orders.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.CategoryId })
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ProductId);
            
            CreateTable(
                "dbo.ShopOrders",
                c => new
                    {
                        ShopOrderId = c.Int(nullable: false, identity: true),
                        SubmissionDate = c.DateTime(nullable: false),
                        OrderStatus_OrderStatusId = c.Int(),
                        Product_ProductId = c.Int(),
                        Customer_CustomerId = c.Int(),
                    })
                .PrimaryKey(t => t.ShopOrderId)
                .ForeignKey("dbo.OrderStatus", t => t.OrderStatus_OrderStatusId)
                .ForeignKey("dbo.Products", t => t.Product_ProductId)
                .ForeignKey("dbo.Customers", t => t.Customer_CustomerId)
                .Index(t => t.OrderStatus_OrderStatusId)
                .Index(t => t.Product_ProductId)
                .Index(t => t.Customer_CustomerId);
            
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        OrderStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.OrderStatusId);
            
            CreateTable(
                "dbo.ProductOrders",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        ShopOrderId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProductId, t.ShopOrderId })
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.ShopOrders", t => t.ShopOrderId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ShopOrderId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ShopOrders", "Customer_CustomerId", "dbo.Customers");
            DropForeignKey("dbo.ShopOrders", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductOrders", "ShopOrderId", "dbo.ShopOrders");
            DropForeignKey("dbo.ProductOrders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ShopOrders", "OrderStatus_OrderStatusId", "dbo.OrderStatus");
            DropForeignKey("dbo.ProductCategories", "ProductId", "dbo.Products");
            DropForeignKey("dbo.ProductCategories", "CategoryId", "dbo.Categories");
            DropIndex("dbo.ProductOrders", new[] { "ShopOrderId" });
            DropIndex("dbo.ProductOrders", new[] { "ProductId" });
            DropIndex("dbo.ShopOrders", new[] { "Customer_CustomerId" });
            DropIndex("dbo.ShopOrders", new[] { "Product_ProductId" });
            DropIndex("dbo.ShopOrders", new[] { "OrderStatus_OrderStatusId" });
            DropIndex("dbo.ProductCategories", new[] { "CategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "ProductId" });
            DropTable("dbo.Customers");
            DropTable("dbo.ProductOrders");
            DropTable("dbo.OrderStatus");
            DropTable("dbo.ShopOrders");
            DropTable("dbo.Products");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Categories");
        }
    }
}
