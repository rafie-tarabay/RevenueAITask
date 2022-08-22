using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
//using RevenueAITask.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueAITask.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            return View();
        }




    }
}
