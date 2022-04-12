using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Module6_7_Palmer.Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Module6_7_Palmer.Areas.Admin.Controllers
{
    [Area("Admins")]
    public class OrderController : Controller
    {
        private OrderContext context { get; set; }
        public OrderController(OrderContext octx)
        {
            context = octx;
        }
        [HttpGet]
        public IActionResult EditOrder(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Order";
            ViewBag.Products = context.Products.OrderBy(p => p.Name);

            var order = context.Orders.
                Include(p => p.Product).FirstOrDefault(o => o.OrderID == id);

            return View(order);
        }
        [HttpPost]
        public IActionResult EditOrder(Order order)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Order";
            string action = (order.OrderID == 0) ? "Action" : "Edit";

            if (ModelState.IsValid)
            {
                if (action == "Add")
                {
                    order.OrderDate = DateTime.Now;
                    context.Orders.Add(order);
                }
                else
                    context.Orders.Update(order);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = action;
                ViewBag.Products = context.Products.OrderBy(o => o.Name);

                return View(order);
            }
        }
        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            ViewBag.Action = "Delete";
            ViewBag.Type = "Order";
            var order = context.Orders.Include(c => c.Customer)
                .Include(p => p.Product)
                .FirstOrDefault(o => o.OrderID == id);

            return View(order);
        }
        [HttpPost]
        public IActionResult DeleteOrder(Order order)
        {
            ViewBag.Action = "Delete";
            ViewBag.Type = "Order";
            context.Orders.Remove(order);
            context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult DisplayOrders()
        {
            ViewBag.Action = "Display";
            ViewBag.Type = "Order";
            var orders = context.Orders.OrderBy(o => o.OrderDate);

            return View(orders);
        }
        //Best way to get the filename
        //public File HandleFile(Product product)
        //{ 
        //    string fileName = Path.GetFileNameWithoutExtension(product.Image);


        //}
    }
}
