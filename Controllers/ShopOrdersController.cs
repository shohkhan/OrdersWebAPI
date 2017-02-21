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
    [RoutePrefix("api/shoporders")]
    public class ShopOrdersController : ApiController
    {
        private OrdersContext db = new OrdersContext();

        [Route("day/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        public List<ProductQuantityDto> GetOrdersOfTheDay(DateTime pubdate)
        {
            return GetOrders(pubdate, 0);
        }

        [Route("week/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        public List<ProductQuantityDto> GetOrdersOfTheWeek(DateTime pubdate)
        {
            return GetOrders(pubdate, 6);
        }

        [Route("month/{pubdate:datetime:regex(\\d{4}-\\d{2}-\\d{2})}")]
        public List<ProductQuantityDto> GetOrdersOfTheMonth(DateTime pubdate)
        {
            return GetOrders(pubdate, 30);
        }

        private List<ProductQuantityDto> GetOrders(DateTime date, int range)
        {
            var ind = Convert.ToInt32(ConfigurationManager.AppSettings.Get("orderNotSubmitted"));
            var date2 = date.AddDays(range + 1);
            //var prods = db.ShopOrders
            //        .Where(o => o.OrderStatus.OrderStatusId != ind &&
            //        o.SubmissionDate >= date && 
            //        o.SubmissionDate < date2)
            //        .Select(x => new OrderDto
            //        {
            //            OrderStatus = x.OrderStatus.Name,
            //            SubmissionDate = x.SubmissionDate,
            //            Products = x.Products
            //                .Select(p => new ProductDto
            //                {
            //                    Name = p.Product.Name,
            //                    UnitPrice = p.Product.Price,
            //                    TotalPrice = p.Product.Price * p.Quantity,
            //                    Quantity = p.Quantity,
            //                    Categories = p.Product.Categories.Select(c => c.Category.Name).ToList(),
            //                    Description = p.Product.Description
            //                }).ToList(),
            //        }).ToList();
            var prods = (from so in db.ShopOrders
                        join po in db.ProductOrders on so.ShopOrderId equals po.ShopOrderId
                        join pd in db.Products on po.ProductId equals pd.ProductId
                        where so.OrderStatus.OrderStatusId != ind &&
                            so.SubmissionDate >= date &&
                            so.SubmissionDate < date2
                        group po by new { pd.Name, pd.Description } into g
                        select new ProductQuantityDto
                        {
                            Name = g.Key.Name,
                            Quantity = g.Sum(po => po.Quantity),
                            Description = g.Key.Description
                        }).ToList();
            return prods;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ShopOrderExists(int id)
        {
            return db.ShopOrders.Count(e => e.ShopOrderId == id) > 0;
        }
    }
}