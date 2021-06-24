using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using onshop.Data;
using onshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using onshop.Utility;

namespace onshop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;
        }

        //GET Checkout actioin method

        public IActionResult Checkout()
        {
            return View();
        }

        //POST Checkout action method

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Checkout(Order anOrder)
        {
            //   List<Products> products = HttpContext.Session.Get <List<Products>("products");
            List<Products> products = HttpContext.Session.Get<List<Products>>("products");
            if (products != null)
            {
                foreach (var product in products)
                {
                    OrderDetail orderDetails = new OrderDetail();
                    orderDetails.ProductID = product.ProductTypesId;
                    anOrder.orderDetails.Add(orderDetails);
                    
                    //_db.OrderDetails.Add(orderDetails);
                }
            }

            anOrder.OrderNo = GetOrderNo();
            //_db.Order.Add(anOrder);
            _db.orders.Add(anOrder);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("products", new List<Products>());
            return View();
        }


        public string GetOrderNo()
        {
            int rowCount = _db.orders.ToList().Count() + 1;
            return rowCount.ToString("000");
        }
    }
}


