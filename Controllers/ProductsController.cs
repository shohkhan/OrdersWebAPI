using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Orders.Models;
using Orders.DTOs;

namespace Orders.Controllers
{
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET: api/Products
        [Route("")]
        public List<ProductDto> GetProducts()
        {
            return db.Products.Select(p =>
            new ProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Categories = p.Categories.Select(c => c.Category.Name).ToList(),
                Description = p.Description,
                UnitPrice = p.Price
            }).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.ProductId == id) > 0;
        }
    }
}