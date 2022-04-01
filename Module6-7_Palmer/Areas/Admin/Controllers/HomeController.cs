using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Module6_7_Palmer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        [Route("[area]/[controller]s/{id?}")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
