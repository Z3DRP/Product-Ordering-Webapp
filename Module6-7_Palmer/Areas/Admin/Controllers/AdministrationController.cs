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
       
        // Controls for Product CRUD
        
      
    }
}
