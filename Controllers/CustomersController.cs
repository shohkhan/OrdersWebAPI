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
using System.Configuration;

namespace Orders.Controllers
{
    [RoutePrefix("api/customers")]
    public class CustomersController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        // GET: api/Customers
        [Route("")]
        public IQueryable<CustomerDto> GetCustomers()
        {
            return db.Customers.Select(c => new CustomerDto
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Address = c.Address,
                Phone = c.Phone,
                Email = c.Email
            });
        }

        // GET: api/Customers/1
        [Route("{id:int}")]
        [ResponseType(typeof(CustomerDto))]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            CustomerDto customer = await db.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new CustomerDto
                {
                    FirstName = c.FirstName,
                    LastName = c.LastName,
                    Address = c.Address,
                    Phone = c.Phone,
                    Email = c.Email
                })
                .FirstOrDefaultAsync();
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //GET: api/Customers/orders
        [Route("orders")]
        public List<OrderDto> GetSubmittedOrdersOfTheCustomer()
        {
            var ind = Convert.ToInt32(ConfigurationManager.AppSettings.Get("orderNotSubmitted"));
            var loggedInUser = Convert.ToInt32(ConfigurationManager.AppSettings.Get("customerId"));

            var orders = db.Customers
                .Where(c => c.CustomerId == loggedInUser)
                .Select(s => s.Orders
                    .Where(o => o.OrderStatus.OrderStatusId != ind)
                    .Select(x => new OrderDto
                    {
                        OrderStatus = x.OrderStatus.Name,
                        SubmissionDate = x.SubmissionDate,
                        Products = x.Products
                            .Select(p => new ProductDto
                            {
                                Name = p.Product.Name,
                                UnitPrice = p.Product.Price,
                                TotalPrice = p.Product.Price * p.Quantity,
                                Quantity = p.Quantity,
                                Categories = p.Product.Categories.Select(c => c.Category.Name).ToList(),
                                Description = p.Product.Description
                            }).ToList(),
                    }).ToList())
                .FirstOrDefault();
            return orders;
        }

        //GET: api/Customers/addtocart/1
        [HttpGet]
        [Route("addtocart/{id:int}")]
        public IHttpActionResult AddToCart(int id)
        {
            var ind = Convert.ToInt32(ConfigurationManager.AppSettings.Get("orderNotSubmitted"));
            var loggedInUser = Convert.ToInt32(ConfigurationManager.AppSettings.Get("customerId"));

            Product pr = db.Products.FirstOrDefault(x => x.ProductId == id);
            if (pr == null)
            {
                return NotFound();
            }
            ProductOrder po = null;
            ShopOrder so = db.Customers.Where(c => c.CustomerId == loggedInUser)
                .Select(s => s.Orders.FirstOrDefault(o => o.OrderStatus.OrderStatusId == ind))
                .FirstOrDefault();
            if (so == null)
            {
                Customer customer = db.Customers.FirstOrDefault(c => c.CustomerId == loggedInUser);

                so = new ShopOrder { OrderStatus = db.OrderStatus.First(x => x.OrderStatusId == ind),
                    SubmissionDate = DateTime.Now
                };
                customer.Orders.Add(so);
            }
            else
            {
                po = db.ProductOrders.Where(x => x.Product.ProductId == id
                        && x.ShopOrderId == so.ShopOrderId).FirstOrDefault();
            }
            if (po != null)
            {
                po.Quantity++;
            }
            else
            {
                po = new ProductOrder { Product = pr, ShopOrder = so, Quantity = 1 };
                db.ProductOrders.Add(po);
            }
            db.SaveChanges();
            return Ok();
        }

        //GET: api/Customers/currentorder
        [Route("currentorder")]
        public OrderDto GetCurrentOrderOfTheCustomer()
        {
            var ind = Convert.ToInt32(ConfigurationManager.AppSettings.Get("orderNotSubmitted"));
            var loggedInUser = Convert.ToInt32(ConfigurationManager.AppSettings.Get("customerId"));

            var order = db.Customers
                .Where(c => c.CustomerId == loggedInUser)
                .Select(s => s.Orders
                    .Where(o => o.OrderStatus.OrderStatusId == ind)
                    .Select(x => new OrderDto
                    {
                        OrderStatus = x.OrderStatus.Name,
                        SubmissionDate = x.SubmissionDate,
                        Products = x.Products
                            .Select(p => new ProductDto
                            {
                                Name = p.Product.Name,
                                UnitPrice = p.Product.Price,
                                TotalPrice = p.Product.Price * p.Quantity,
                                Quantity = p.Quantity,
                                Categories = p.Product.Categories.Select(c => c.Category.Name).ToList(),
                                Description = p.Product.Description
                            }).ToList(),
                    }).FirstOrDefault())
                .FirstOrDefault();
            return order;
        }

        //GET: api/Customers/submitorder
        [HttpGet]
        [Route("submitorder")]
        public IHttpActionResult GetSubmitCurrentOrder()
        {
            var ind = Convert.ToInt32(ConfigurationManager.AppSettings.Get("orderNotSubmitted"));
            var loggedInUser = Convert.ToInt32(ConfigurationManager.AppSettings.Get("customerId"));
            var submitted = Convert.ToInt32(ConfigurationManager.AppSettings.Get("orderSubmitted"));

            ShopOrder order = db.Customers
                .Where(c => c.CustomerId == loggedInUser)
                .Select(s => s.Orders
                    .Where(o => o.OrderStatus.OrderStatusId == ind).FirstOrDefault()).FirstOrDefault();
            if (order == null)
            {
                return NotFound();
            }
            OrderStatus os = db.OrderStatus.First(x => x.OrderStatusId == submitted);
            order.OrderStatus = os;
            order.SubmissionDate = DateTime.Now;
            db.SaveChanges();
            return Ok();
        }

        //GET: api/Customers/exercise
        [Route("exercise")]
        public List<ExerciseDto> GetExerciseResult()
        {
            var loggedInUser = Convert.ToInt32(ConfigurationManager.AppSettings.Get("customerId"));

            List<ExerciseDto> a = (from c in db.Customers
                    from o in c.Orders
                    join po in db.ProductOrders on o.ShopOrderId equals po.ShopOrderId
                    join pd in db.Products on po.ProductId equals pd.ProductId
                    from ct in pd.Categories
                    join ca in db.Categories on ct.CategoryId equals ca.CategoryId
                    where c.CustomerId == loggedInUser
                    group po by new { c.FirstName, c.CustomerId, ca.CategoryId, ca.Name } into g
                    select new ExerciseDto
                    {
                        CustomerId = g.Key.CustomerId,
                        CustomerFirstName = g.Key.FirstName,
                        CategoryId = g.Key.CategoryId,
                        CategoryName = g.Key.Name,
                        NumberPurchased = g.Sum(po => po.Quantity),
                    }).ToList();
            return a;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomerExists(int id)
        {
            return db.Customers.Count(e => e.CustomerId == id) > 0;
        }
    }
}