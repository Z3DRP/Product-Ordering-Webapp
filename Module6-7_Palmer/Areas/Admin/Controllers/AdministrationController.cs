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
    [Area("Admin")]
    public class AdministrationController : Controller
    {
        private OrderContext context { get; set; }

        public AdministrationController(OrderContext octx)
        {
            context = octx;
        }
        // set this up like nfl example 1 with the different actions to call diff methods for Crud on dif Models

        //Controls for Customer CRUD

        public IActionResult Main()
        {
            return View("Main");

        }
        [HttpGet]
        public IActionResult AddCustomer()
        {
            ViewBag.Action = "Add";
            ViewBag.Type = "Customer";
            return View("Edit", new Customer());
        }
        [HttpGet]
        public IActionResult EditCustomer(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Customer";
            var customer = context.Customers
                .FirstOrDefault(c => c.CustomerID == id);

            return View(customer);
        }
        [HttpPost]
        public IActionResult EditCustomer(Customer customer)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Customer";
            string action = (customer.CustomerID == 0) ? "Add" : "Edit";

            if (ModelState.IsValid)
            {
                if (action == "Add")
                    context.Customers.Add(customer);
                else
                    context.Customers.Update(customer);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = action;
                return View(customer);
            }
        }
        [HttpGet]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == id);
            ViewBag.Action = "Delete";
            ViewBag.Type = "Customer";
            return View(customer);
        }
        [HttpPost]
        public IActionResult DeleteCustomer(Customer customer)
        {
            ViewBag.ActionType = "Delete";
            ViewBag.Type = "Customer";
            context.Customers.Remove(customer);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult CustomerDetails(int id)
        {
            ViewBag.Action = "Delete";
            ViewBag.Type = "Customer";
            var customer = context.Customers.FirstOrDefault(c => c.CustomerID == id);

            return View(customer);
        }
        public IActionResult DisplayCutomers()
        {
            ViewBag.Action = "Display";
            ViewBag.Type = "Customer";
            var customer = context.Customers.OrderBy(c => c.LastName);

            return View(customer);
        }
        // Controls for Product CRUD
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Action = "Add";
            ViewBag.Type = "Product";
            return View("Edit", new Product());
        }
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Product";
            var product = context.Products.FirstOrDefault(p => p.ProductID == id);

            return View(product);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Product";
            string action = (product.ProductID == 0) ? "Add" : "Edit";

            string testImg = product.Image;

            if (ModelState.IsValid)
            {
                if (action == "Add")
                    context.Products.Add(product);
                else
                    context.Products.Update(product);
                context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Action = action;

                return View(product);
            }
        }
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            ViewBag.Action = "Delete";
            ViewBag.Type = "Product";
            var product = context.Products.FirstOrDefault(p => p.ProductID == id);

            return View(product);
        }
        [HttpPost]
        public IActionResult DeleteProduct(Product product)
        {
            ViewBag.Action = "Delete";
            ViewBag.Type = "Product";
            context.Products.Remove(product);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ProductDetails(int id)
        {
            ViewBag.Action = "Details";
            ViewBag.Type = "Product";
            var product = context.Products.FirstOrDefault(p => p.ProductID == id);

            return View(product);
        }
        public IActionResult DisplayProducts()
        {
            ViewBag.Action = "Display";
            ViewBag.Type = "Product";
            var products = context.Products.OrderBy(p => p.Name);

            return View(products);
        }
        public IActionResult SortProductsByPrice()
        {
            ViewBag.Action = "Sort";
            ViewBag.Type = "Product";
            var products = context.Products.OrderByDescending(p => p.UnitPrice);
            return View(products);
        }
        // Controls for Order CRUD
        [HttpGet]
        public IActionResult AddOrder()
        {
            ViewBag.Action = "Add";
            ViewBag.Type = "Order";
            ViewBag.Products = context.Products.OrderBy(p => p.Name);

            return View("Edit", new Order());
        }
        [HttpGet]
        public IActionResult EditOrder(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Type = "Order";
            ViewBag.Products = context.Products.OrderBy(p => p.Name);

            var order = context.Orders.
                Include(p=>p.Product).FirstOrDefault(o => o.OrderID == id);

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
