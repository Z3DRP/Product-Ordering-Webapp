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
    public class ProductController : Controller
    {
        private OrderContext context { get; set; }

        public ProductController(OrderContext pctx)
        {
            context = pctx;
        }
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
    }
}
